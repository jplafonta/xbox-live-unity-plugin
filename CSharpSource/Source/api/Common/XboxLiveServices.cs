﻿// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    using global::System;
    using global::System.Runtime.InteropServices;
    using Microsoft.Xbox.Services.Privacy;
    using Microsoft.Xbox.Services.System;
    using Microsoft.Xbox.Services.TitleStorage;

    public partial class XboxLiveServices
    {
        public PrivacyService PrivacyService { get; private set; }
        public TitleStorageService TitleStorageService { get; private set; }
        
        public XboxLiveServices(XboxLiveUser user)
        {
#if UNITY_EDITOR
            // TODO Implement mock title storage service for unity editor
            this.TitleStorageService = null;
#else
            IntPtr xboxLiveContext;
            var xsapiResult = XboxLiveContextCreate(user.PCXboxLiveUser, out xboxLiveContext);

            if (xsapiResult != XSAPI_RESULT.XSAPI_RESULT_OK)
            {
                throw new XboxException(xsapiResult);
            }
            XboxLiveContextPtr = xboxLiveContext;

            this.TitleStorageService = new TitleStorageService(XboxLiveContextPtr);
#endif
            this.PrivacyService = new PrivacyService(user);
        }

        ~XboxLiveServices()
        {
            XboxLiveContextDelete(XboxLiveContextPtr);
        }

        internal IntPtr XboxLiveContextPtr { get; private set; }


        [DllImport(XboxLive.FlatCDllName)]
        internal static extern XSAPI_RESULT XboxLiveContextCreate(IntPtr xboxLiveUser, out IntPtr xboxLiveContext);

        [DllImport(XboxLive.FlatCDllName)]
        internal static extern void XboxLiveContextDelete(IntPtr xboxLiveContext);
    }
}