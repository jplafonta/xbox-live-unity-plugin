// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Matchmaking
#else
namespace Microsoft.Xbox.Services.Matchmaking
#endif
{
    public enum TicketStatus : int
    {
        Unknown = 0,
        Expired = 1,
        Searching = 2,
        Found = 3,
        Canceled = 4,
    }

}
