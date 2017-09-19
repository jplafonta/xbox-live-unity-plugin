// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Statistics.Manager
#else
namespace Microsoft.Xbox.Services.Statistics.Manager
#endif
{
    public enum StatEventType
    {
        LocalUserAdded,
        LocalUserRemoved,
        StatUpdateComplete,
        GetLeaderboardComplete,
    }
}