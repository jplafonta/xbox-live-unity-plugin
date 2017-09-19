// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.System
#else
namespace Microsoft.Xbox.Services.System
#endif
{
    using global::System;
    using global::System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct XboxLiveResult
    {
        [MarshalAsAttribute(UnmanagedType.I4)]
        public int errorCode;

        [MarshalAsAttribute(UnmanagedType.LPWStr)]
        public String errorMessage;
    }
}
