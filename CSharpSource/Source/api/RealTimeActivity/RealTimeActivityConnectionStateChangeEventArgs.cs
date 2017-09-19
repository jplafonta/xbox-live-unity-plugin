// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.RealTimeActivity
#else
namespace Microsoft.Xbox.Services.RealTimeActivity
#endif
{
    public class RealTimeActivityConnectionStateChangeEventArgs : EventArgs
    {
    }
}
