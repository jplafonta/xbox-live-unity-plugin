// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.GameServerPlatform
#else
namespace Microsoft.Xbox.Services.GameServerPlatform
#endif
{
    public class QualityOfServiceServer
    {

        public string TargetLocation
        {
            get;
            private set;
        }

        public string SecureDeviceAddressBase64
        {
            get;
            private set;
        }

        public string ServerFullQualifiedDomainName
        {
            get;
            private set;
        }

    }
}
