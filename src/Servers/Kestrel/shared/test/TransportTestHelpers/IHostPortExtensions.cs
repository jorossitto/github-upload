// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Microsoft.AspNetCore.Hosting
{
    public static class IHostPortExtensions
    {
        public static int GetPort(this IHost host)
        {
            return host.GetPorts().First();
        }

        public static IEnumerable<int> GetPorts(this IHost host)
        {
            return host.GetUris()
                .Select(u => u.Port);
        }

        public static IEnumerable<Uri> GetUris(this IHost host)
        {
            return host.Services.GetRequiredService<IServer>().Features.Get<IServerAddressesFeature>().Addresses
                .Select(a => new Uri(a));
        }

        public static string MakeUrl(this IHost host, string scheme)
        {
            return $"{scheme}://127.0.0.1:{host.GetPort().ToString(CultureInfo.InvariantCulture)}/";
        }
    }
}
