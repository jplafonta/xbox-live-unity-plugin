// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public class DevicePresenceChangeSubscription
    {

        public string XboxUserId
        {
            get;
            private set;
        }

        public uint SubscriptionId
        {
            get;
            private set;
        }

        public string ResourceUri
        {
            get;
            private set;
        }

        public Microsoft.Xbox.Services.RealTimeActivity.RealTimeActivitySubscriptionState State
        {
            get;
            private set;
        }

    }
}
