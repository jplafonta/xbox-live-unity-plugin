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

    public class GameServerMetadataResult
    {

        public IList<GameServerImageSet> GameServerImageSets
        {
            get;
            private set;
        }

        public IList<GameVariant> GameVariants
        {
            get;
            private set;
        }

    }
}
