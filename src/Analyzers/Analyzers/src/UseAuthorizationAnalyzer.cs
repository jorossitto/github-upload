// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Microsoft.AspNetCore.Analyzers
{
    internal class UseAuthorizationAnalyzer
    {
        private readonly StartupAnalysis _context;

        public UseAuthorizationAnalyzer(StartupAnalysis context)
        {
            _context = context;
        }

        public void AnalyzeSymbol(SymbolAnalysisContext context)
        {
            Debug.Assert(context.Symbol.Kind == SymbolKind.NamedType);
            Debug.Assert(StartupFacts.IsStartupClass(_context.StartupSymbols, (INamedTypeSymbol)context.Symbol));

            var type = (INamedTypeSymbol)context.Symbol;

            foreach (var middlewareAnalysis in _context.GetRelatedAnalyses<MiddlewareAnalysis>(type))
            {
                MiddlewareItem? useAuthorizationItem = default;
                MiddlewareItem? useRoutingItem = default;

                var length = middlewareAnalysis.Middleware.Length;
                for (var i = length - 1; i >= 0; i-- )
                {
                    var middlewareItem = middlewareAnalysis.Middleware[i];
                    var middleware = middlewareItem.UseMethod.Name;

                    if (middleware == "UseAuthorization")
                    {
                        if (useRoutingItem != null && useAuthorizationItem == null)
                        {
                            // This looks like
                            // 
                            //  app.UseAuthorization();
                            //  ...
                            //  app.UseRouting();
                            //  app.UseEndpoints(...);

                            context.ReportDiagnostic(Diagnostic.Create(
                                StartupAnalyzer.Diagnostics.IncorrectlyConfiguredAuthorizationMiddleware,
                                middlewareItem.Operation.Syntax.GetLocation(),
                                middlewareItem.UseMethod.Name));
                        }

                        useAuthorizationItem = middlewareItem;
                    }
                    else if (middleware == "UseEndpoints")
                    {
                        if (useAuthorizationItem != null)
                        {
                            // This configuration looks like
                            // 
                            //  app.UseRouting();
                            //  app.UseEndpoints(...);
                            //  ...
                            //  app.UseAuthorization();
                            //

                            context.ReportDiagnostic(Diagnostic.Create(
                                StartupAnalyzer.Diagnostics.IncorrectlyConfiguredAuthorizationMiddleware,
                                useAuthorizationItem.Operation.Syntax.GetLocation(),
                                middlewareItem.UseMethod.Name));
                        }
                    }
                    else if (middleware == "UseRouting")
                    {
                        useRoutingItem = middlewareItem;
                    }
                }
            }
        }
    }
}
