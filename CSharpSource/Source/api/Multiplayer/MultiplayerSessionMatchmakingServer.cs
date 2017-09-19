// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Multiplayer
#else
namespace Microsoft.Xbox.Services.Multiplayer
#endif
{
    public class MultiplayerSessionMatchmakingServer
    {

        public MultiplayerSessionReference TargetSessionRef
        {
            get;
            private set;
        }

        public TimeSpan TypicalWait
        {
            get;
            private set;
        }

        public string StatusDetails
        {
            get;
            private set;
        }

        public MatchmakingStatus Status
        {
            get;
            private set;
        }

    }
}
