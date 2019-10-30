// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.AspNetCore.Authentication.OAuth
{
    /// <summary>
    /// Contains information used to perform the code exchange.
    /// </summary>
    public class OAuthCodeExchangeContext
    {
        /// <summary>
        /// Initializes a new <see cref="OAuthCodeExchangeContext"/>.
        /// </summary>
        /// <param name="properties">The <see cref="AuthenticationProperties"/>.</param>
        /// <param name="code">The code returned from the authorization endpoint.</param>
        /// <param name="redirectUri">The redirect uri used in the authorization request.</param>
        public OAuthCodeExchangeContext(AuthenticationProperties properties, string code, string redirectUri)
        {
            Properties = properties;
            Code = code;
            RedirectUri = redirectUri;
        }

        /// <summary>
        /// State for the authentication flow.
        /// </summary>
        public AuthenticationProperties Properties { get; }

        /// <summary>
        /// The code returned from the authorization endpoint.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The redirect uri used in the authorization request.
        /// </summary>
        public string RedirectUri { get; }
    }
}
