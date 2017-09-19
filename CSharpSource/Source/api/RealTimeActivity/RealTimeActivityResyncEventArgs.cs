// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.RealTimeActivity
#else
namespace Microsoft.Xbox.Services.RealTimeActivity
#endif
{
    public class RealTimeActivityResyncEventArgs : EventArgs
    {
    }
}
