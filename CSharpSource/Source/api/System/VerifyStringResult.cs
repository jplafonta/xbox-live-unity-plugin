// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.System
#else
namespace Microsoft.Xbox.Services.System
#endif
{
    public class VerifyStringResult
    {

        public string FirstOffendingSubstring
        {
            get;
            private set;
        }

        public VerifyStringResultCode ResultCode
        {
            get;
            private set;
        }

    }
}
