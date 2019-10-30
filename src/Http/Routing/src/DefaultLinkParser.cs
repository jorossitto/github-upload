// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Routing
{
    internal class DefaultLinkParser : LinkParser, IDisposable
    {
        private readonly ParameterPolicyFactory _parameterPolicyFactory;
        private readonly ILogger<DefaultLinkParser> _logger;
        private readonly IServiceProvider _serviceProvider;

        // Caches RoutePatternMatcher instances
        private readonly DataSourceDependentCache<ConcurrentDictionary<RouteEndpoint, MatcherState>> _matcherCache;

        // Used to initialize RoutePatternMatcher and constraint instances
        private readonly Func<RouteEndpoint, MatcherState> _createMatcher;

        public DefaultLinkParser(
            ParameterPolicyFactory parameterPolicyFactory,
            EndpointDataSource dataSource,
            ILogger<DefaultLinkParser> logger,
            IServiceProvider serviceProvider)
        {
            _parameterPolicyFactory = parameterPolicyFactory;
            _logger = logger;
            _serviceProvider = serviceProvider;

            // We cache RoutePatternMatcher instances per-Endpoint for performance, but we want to wipe out
            // that cache is the endpoints change so that we don't allow unbounded memory growth.
            _matcherCache = new DataSourceDependentCache<ConcurrentDictionary<RouteEndpoint, MatcherState>>(dataSource, (_) =>
            {
                // We don't eagerly fill this cache because there's no real reason to. Unlike URL matching, we don't
                // need to build a big data structure up front to be correct.
                return new ConcurrentDictionary<RouteEndpoint, MatcherState>();
            });

            // Cached to avoid per-call allocation of a delegate on lookup.
            _createMatcher = CreateRoutePatternMatcher;
        }

        public override RouteValueDictionary ParsePathByAddress<TAddress>(TAddress address, PathString path)
        {
            var endpoints = GetEndpoints(address);
            if (endpoints.Count == 0)
            {
                return null;
            }

            for (var i = 0; i < endpoints.Count; i++)
            {
                var endpoint = endpoints[i];
                if (TryParse(endpoint, path, out var values))
                {
                    Log.PathParsingSucceeded(_logger, path, endpoint);
                    return values;
                }
            }

            Log.PathParsingFailed(_logger, path, endpoints);
            return null;
        }

        private List<RouteEndpoint> GetEndpoints<TAddress>(TAddress address)
        {
            var addressingScheme = _serviceProvider.GetRequiredService<IEndpointAddressScheme<TAddress>>();
            var endpoints = addressingScheme.FindEndpoints(address).OfType<RouteEndpoint>().ToList();

            if (endpoints.Count == 0)
            {
                Log.EndpointsNotFound(_logger, address);
            }
            else
            {
                Log.EndpointsFound(_logger, address, endpoints);
            }

            return endpoints;
        }

        private MatcherState CreateRoutePatternMatcher(RouteEndpoint endpoint)
        {
            var constraints = new Dictionary<string, List<IRouteConstraint>>(StringComparer.OrdinalIgnoreCase);

            var policies = endpoint.RoutePattern.ParameterPolicies;
            foreach (var kvp in policies)
            {
                var constraintsForParameter = new List<IRouteConstraint>();
                var parameter = endpoint.RoutePattern.GetParameter(kvp.Key);
                for (var i = 0; i < kvp.Value.Count; i++)
                {
                    var policy = _parameterPolicyFactory.Create(parameter, kvp.Value[i]);
                    if (policy is IRouteConstraint constraint)
                    {
                        constraintsForParameter.Add(constraint);
                    }
                }

                if (constraintsForParameter.Count > 0)
                {
                    constraints.Add(kvp.Key, constraintsForParameter);
                }
            }

            var matcher = new RoutePatternMatcher(endpoint.RoutePattern, new RouteValueDictionary(endpoint.RoutePattern.Defaults));
            return new MatcherState(matcher, constraints);
        }

        // Internal for testing
        internal MatcherState GetMatcherState(RouteEndpoint endpoint) => _matcherCache.EnsureInitialized().GetOrAdd(endpoint, _createMatcher);

        // Internal for testing
        internal bool TryParse(RouteEndpoint endpoint, PathString path, out RouteValueDictionary values)
        {
            var (matcher, constraints) = GetMatcherState(endpoint);

            values = new RouteValueDictionary();
            if (!matcher.TryMatch(path, values))
            {
                values = null;
                return false;
            }

            foreach (var kvp in constraints)
            {
                for (var i = 0; i < kvp.Value.Count; i++)
                {
                    var constraint = kvp.Value[i];
                    if (!constraint.Match(httpContext: null, NullRouter.Instance, kvp.Key, values, RouteDirection.IncomingRequest))
                    {
                        values = null;
                        return false;
                    }
                }
            }

            return true;
        }

        public void Dispose()
        {
            _matcherCache.Dispose();
        }

        // internal for testing
        internal readonly struct MatcherState
        {
            public readonly RoutePatternMatcher Matcher;
            public readonly Dictionary<string, List<IRouteConstraint>> Constraints;

            public MatcherState(RoutePatternMatcher matcher, Dictionary<string, List<IRouteConstraint>> constraints)
            {
                Matcher = matcher;
                Constraints = constraints;
            }

            public void Deconstruct(out RoutePatternMatcher matcher, out Dictionary<string, List<IRouteConstraint>> constraints)
            {
                matcher = Matcher;
                constraints = Constraints;
            }
        }

        private static class Log
        {
            public static class EventIds
            {
                public static readonly EventId EndpointsFound = new EventId(100, "EndpointsFound");
                public static readonly EventId EndpointsNotFound = new EventId(101, "EndpointsNotFound");

                public static readonly EventId PathParsingSucceeded = new EventId(102, "PathParsingSucceeded");
                public static readonly EventId PathParsingFailed = new EventId(103, "PathParsingFailed");
            }

            private static readonly Action<ILogger, IEnumerable<string>, object, Exception> _endpointsFound = LoggerMessage.Define<IEnumerable<string>, object>(
                LogLevel.Debug,
                EventIds.EndpointsFound,
                "Found the endpoints {Endpoints} for address {Address}");

            private static readonly Action<ILogger, object, Exception> _endpointsNotFound = LoggerMessage.Define<object>(
                LogLevel.Debug,
                EventIds.EndpointsNotFound,
                "No endpoints found for address {Address}");

            private static readonly Action<ILogger, string, string, Exception> _pathParsingSucceeded = LoggerMessage.Define<string, string>(
                LogLevel.Debug,
                EventIds.PathParsingSucceeded,
                "Path parsing succeeded for endpoint {Endpoint} and URI path {URI}");

            private static readonly Action<ILogger, IEnumerable<string>, string, Exception> _pathParsingFailed = LoggerMessage.Define<IEnumerable<string>, string>(
                LogLevel.Debug,
                EventIds.PathParsingFailed,
                "Path parsing failed for endpoints {Endpoints} and URI path {URI}");

            public static void EndpointsFound(ILogger logger, object address, IEnumerable<Endpoint> endpoints)
            {
                // Checking level again to avoid allocation on the common path
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    _endpointsFound(logger, endpoints.Select(e => e.DisplayName), address, null);
                }
            }

            public static void EndpointsNotFound(ILogger logger, object address)
            {
                _endpointsNotFound(logger, address, null);
            }

            public static void PathParsingSucceeded(ILogger logger, PathString path, Endpoint endpoint)
            {
                // Checking level again to avoid allocation on the common path
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    _pathParsingSucceeded(logger, endpoint.DisplayName, path.Value, null);
                }
            }

            public static void PathParsingFailed(ILogger logger, PathString path, IEnumerable<Endpoint> endpoints)
            {
                // Checking level again to avoid allocation on the common path
                if (logger.IsEnabled(LogLevel.Debug))
                {
                    _pathParsingFailed(logger, endpoints.Select(e => e.DisplayName), path.Value, null);
                }
            }
        }
    }
}
