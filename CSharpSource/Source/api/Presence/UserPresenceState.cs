// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public enum UserPresenceState
    {
        Unknown = 0,
        Online = 1,
        Away = 2,
        Offline = 3,
    }
}