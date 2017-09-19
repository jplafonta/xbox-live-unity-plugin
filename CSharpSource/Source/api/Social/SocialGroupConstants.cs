// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Social
#else
namespace Microsoft.Xbox.Services.Social
#endif
{
    public class SocialGroupConstants
    {
        public SocialGroupConstants() {
        }

        public static string People
        {
            get;
            private set;
        }

        public static string Favorite
        {
            get;
            private set;
        }

    }
}
