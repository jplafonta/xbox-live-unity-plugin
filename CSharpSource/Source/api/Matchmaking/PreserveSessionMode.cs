// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Matchmaking
#else
namespace Microsoft.Xbox.Services.Matchmaking
#endif
{
    public enum PreserveSessionMode : int
    {
        Unknown = 0,
        Always = 1,
        Never = 2,
    }

}
