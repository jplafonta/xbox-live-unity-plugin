// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Multiplayer
#else
namespace Microsoft.Xbox.Services.Multiplayer
#endif
{
    public enum MultiplayMetrics : int
    {
        Unknown = 0,
        BandwidthUp = 1,
        BandwidthDown = 2,
        Bandwidth = 3,
        Latency = 4,
    }

}
