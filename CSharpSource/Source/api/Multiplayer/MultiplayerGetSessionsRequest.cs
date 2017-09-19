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
    public class MultiplayerGetSessionsRequest
    {
        public MultiplayerGetSessionsRequest(string serviceConfigurationId, uint maxItems) {
        }

        public uint ContractVersionFilter
        {
            get;
            set;
        }

        public MultiplayerSessionVisibility VisibilityFilter
        {
            get;
            set;
        }

        public string SessionTemplateNameFilter
        {
            get;
            set;
        }

        public string KeywordFilter
        {
            get;
            set;
        }

        public IList<string> XboxUserIdsFilter
        {
            get;
            set;
        }

        public string XboxUserIdFilter
        {
            get;
            set;
        }

        public bool IncludeInactiveSessions
        {
            get;
            set;
        }

        public bool IncludeReservations
        {
            get;
            set;
        }

        public bool IncludePrivateSessions
        {
            get;
            set;
        }

        public uint MaxItems
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
