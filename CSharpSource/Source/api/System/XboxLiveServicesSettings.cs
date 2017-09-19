// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.System
#else
namespace Microsoft.Xbox.Services.System
#endif
{
    using global::System;

    public class XboxLiveServicesSettings
    {
        public XboxServicesDiagnosticsTraceLevel DiagnosticsTraceLevel { get; set; }

        public static XboxLiveServicesSettings SingletonInstance { get; private set; }

        //public event EventHandler<XboxLiveLogCallEventArgs> LogCallRouted;
    }
}