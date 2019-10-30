// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.IIS;

namespace Microsoft.AspNetCore.Builder
{
    public class IISServerOptions
    {
        /// <summary>
        /// Gets or sets a value that controls whether synchronous IO is allowed for the <see cref="HttpContext.Request"/> and <see cref="HttpContext.Response"/>
        /// </summary>
        /// <remarks>
        /// Defaults to false.
        /// </remarks>
        public bool AllowSynchronousIO { get; set; } = false;

        /// <summary>
        /// If true the server should set HttpContext.User. If false the server will only provide an
        /// identity when explicitly requested by the AuthenticationScheme.
        /// Note Windows Authentication must also be enabled in IIS for this to work.
        /// </summary>
        public bool AutomaticAuthentication { get; set; } = true;

        /// <summary>
        /// Sets the display name shown to users on login pages. The default is null.
        /// </summary>
        public string AuthenticationDisplayName { get; set; }

        /// <summary>
        /// Used to indicate if the authentication handler should be registered. This is only done if ANCM indicates
        /// IIS has a non-anonymous authentication enabled, or for back compat with ANCMs that did not provide this information.
        /// </summary>
        internal bool ForwardWindowsAuthentication { get; set; } = true;

        internal string[] ServerAddresses { get; set; }

        // Matches the default maxAllowedContentLength in IIS (~28.6 MB)
        // https://www.iis.net/configreference/system.webserver/security/requestfiltering/requestlimits#005
        private long? _maxRequestBodySize = 30000000;

        internal long IisMaxRequestSizeLimit;

        /// <summary>
        /// Gets or sets the maximum allowed size of any request body in bytes.
        /// When set to null, the maximum request body size is unlimited.
        /// This limit has no effect on upgraded connections which are always unlimited.
        /// This can be overridden per-request via <see cref="IHttpMaxRequestBodySizeFeature"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to null (unlimited).
        /// </remarks>
        public long? MaxRequestBodySize
        {
            get => _maxRequestBodySize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), CoreStrings.NonNegativeNumberOrNullRequired);
                }
                _maxRequestBodySize = value;
            }
        }
    }
}
