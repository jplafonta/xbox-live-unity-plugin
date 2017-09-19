// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.GameServerPlatform
#else
namespace Microsoft.Xbox.Services.GameServerPlatform
#endif
{
    public enum GameServerFulfillmentState : int
    {
        Unknown = 0,
        Fulfilled = 1,
        Queued = 2,
        Aborted = 3,
    }

}
