// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.GameServerPlatform
#else
namespace Microsoft.Xbox.Services.GameServerPlatform
#endif
{
    using global::System.Collections.Generic;

    public class GameServerImageSet
    {

        public Dictionary<string, string> Tags
        {
            get;
            private set;
        }

        public string SchemaId
        {
            get;
            private set;
        }

        public string SchemaName
        {
            get;
            private set;
        }

        public string SchemaContent
        {
            get;
            private set;
        }

        public ulong SelectionOrder
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Id
        {
            get;
            private set;
        }

        public ulong MaxPlayers
        {
            get;
            private set;
        }

        public ulong MinPlayers
        {
            get;
            private set;
        }

    }
}
