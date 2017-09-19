// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Privacy
#else
namespace Microsoft.Xbox.Services.Privacy
#endif
{
    using Newtonsoft.Json;

    public class PermissionReason
    {
        [JsonProperty("restrictedSetting")]
        public string RestrictedSetting { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

    }
}
