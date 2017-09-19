// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.GameServerPlatform
#else
namespace Microsoft.Xbox.Services.GameServerPlatform
#endif
{
    public class GameVariant
    {

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

        public ulong Rank
        {
            get;
            private set;
        }

        public bool IsPublisher
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

    }
}
