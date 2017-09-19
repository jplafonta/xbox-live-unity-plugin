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
    public enum PresenceTitleViewState : int
    {
        Unknown = 0,
        FullScreen = 1,
        Filled = 2,
        Snapped = 3,
        Background = 4,
    }

}
