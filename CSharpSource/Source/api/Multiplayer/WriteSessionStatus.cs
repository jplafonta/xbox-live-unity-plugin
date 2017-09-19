// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Multiplayer
#else
namespace Microsoft.Xbox.Services.Multiplayer
#endif
{
    public enum WriteSessionStatus : int
    {
        Unknown = 0,
        AccessDenied = 1,
        Created = 2,
        Conflict = 3,
        HandleNotFound = 4,
        OutOfSync = 5,
        SessionDeleted = 6,
        Updated = 7,
    }

}
