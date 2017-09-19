// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Collections.Generic;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.GameServerPlatform
#else
namespace Microsoft.Xbox.Services.GameServerPlatform
#endif
{
    public class ClusterResult
    {

        public string SecureDeviceAddress
        {
            get;
            private set;
        }

        public IList<GameServerPortMapping> PortMappings
        {
            get;
            private set;
        }

        public string Region
        {
            get;
            private set;
        }

        public string HostName
        {
            get;
            private set;
        }

        public GameServerFulfillmentState FulfillmentState
        {
            get;
            private set;
        }

        public TimeSpan PollInterval
        {
            get;
            private set;
        }

    }
}
