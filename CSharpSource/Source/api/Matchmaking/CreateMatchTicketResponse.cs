// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Matchmaking
#else
namespace Microsoft.Xbox.Services.Matchmaking
#endif
{
    using global::System;

    public class CreateMatchTicketResponse
    {

        public TimeSpan EstimatedWaitTime
        {
            get;
            private set;
        }

        public string MatchTicketId
        {
            get;
            private set;
        }

    }
}
