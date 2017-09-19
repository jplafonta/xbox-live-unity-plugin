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
    public class PresenceData
    {
        public PresenceData(string serviceConfigurationId, string presenceId, string[] presenceTokenIds) {
            PresenceId = presenceId;
            ServiceConfigurationId = serviceConfigurationId;
            PresenceTokenIds = presenceTokenIds;
        }
        public PresenceData(string serviceConfigurationId, string presenceId) {
            PresenceId = presenceId;
            ServiceConfigurationId = serviceConfigurationId;
        }

        public PresenceData() { }

        public IList<string> PresenceTokenIds
        {
            get;
            private set;
        }

        public string PresenceId
        {
            get;
            private set;
        }

        public string ServiceConfigurationId
        {
            get;
            private set;
        }

    }
}
