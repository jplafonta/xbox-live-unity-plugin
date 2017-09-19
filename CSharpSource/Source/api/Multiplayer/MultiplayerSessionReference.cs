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
    public class MultiplayerSessionReference
    {
        public MultiplayerSessionReference(string serviceConfigurationId, string sessionTemplateName, string sessionName) {
        }

        public string SessionName
        {
            get;
            private set;
        }

        public string SessionTemplateName
        {
            get;
            private set;
        }

        public string ServiceConfigurationId
        {
            get;
            private set;
        }


        public static MultiplayerSessionReference ParseFromUriPath(string uriPath)
        {
            throw new NotImplementedException();
        }


        public string ToUriPath()
        {
            throw new NotImplementedException();
        }

    }
}
