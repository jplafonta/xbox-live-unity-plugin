// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.GameServerPlatform
#else
namespace Microsoft.Xbox.Services.GameServerPlatform
#endif
{
    public class GameServerPortMapping
    {

        public uint ExternalPortNumber
        {
            get;
            private set;
        }

        public uint InternalPortNumber
        {
            get;
            private set;
        }

        public string PortName
        {
            get;
            private set;
        }

    }
}
