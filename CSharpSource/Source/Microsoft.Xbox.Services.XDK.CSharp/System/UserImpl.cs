// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Plugin.Microsoft.Xbox.Services.System
{
    using global::System;
    using global::System.Runtime.InteropServices;
    using global::System.Threading.Tasks;

    internal class UserImpl : IUserImpl
    {
        public bool IsSignedIn
        {
            get { return xboxSystemUser.IsSignedIn; }
        }
        public string XboxUserId
        {
            get { return xboxSystemUser.XboxUserId; }
        }
        public string Gamertag
        {
            get { return xboxSystemUser.DisplayInfo.Gamertag; }
        }
        public string AgeGroup
        {
            get { return xboxSystemUser.DisplayInfo.AgeGroup.ToString(); }
        }
        public string Privileges
        {
            get
            {
                // TODO translate Windows.Xbox.System.User.DisplayInfo.Privileges to a string
                throw new NotImplementedException();
            }
        }
        public AuthConfig AuthConfig
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IntPtr XboxLiveUserPtr { get; private set; }

        private Windows.Xbox.System.User xboxSystemUser;

        public UserImpl(Windows.Xbox.System.User xboxSystemUser)
        {
            if (xboxSystemUser == null)
            {
                throw new ArgumentException("XboxSystemUser cannot be null!");
            }
            this.xboxSystemUser = xboxSystemUser;

            IntPtr xboxLiveUserPtr;
            var result = XboxLiveUserCreate(Marshal.GetIUnknownForObject(this.xboxSystemUser),  out xboxLiveUserPtr);

            if (result != XSAPI_RESULT.XSAPI_RESULT_OK)
            {
                throw new XboxException(result);
            }

            this.XboxLiveUserPtr = xboxLiveUserPtr;
        }

        public Task<SignInResult> SignInImpl(bool showUI, bool forceRefresh)
        {
            throw new NotImplementedException();
        }

        public Task<TokenAndSignatureResult> InternalGetTokenAndSignatureAsync(string httpMethod, string url, string headers, byte[] body, bool promptForCredentialsIfNeeded, bool forceRefresh)
        {
            return Task.Run(() =>
            {
                var getTokenAndSignatureResult = xboxSystemUser.GetTokenAndSignatureAsync(httpMethod, url, headers, body).GetResults();

                return new TokenAndSignatureResult
                {
                    Token = getTokenAndSignatureResult.Token,
                    Signature = getTokenAndSignatureResult.Signature
                };
            });
        }

        [DllImport(XboxLive.FlatCDllName)]
        private static extern XSAPI_RESULT XboxLiveUserCreate(
            IntPtr xboxSystemUser,
            out IntPtr xboxLiveUserPtr);

        [StructLayout(LayoutKind.Sequential)]
        private struct XSAPI_XBOX_LIVE_USER
        {
            public IntPtr XboxUserId;
            public IntPtr Gamertag;
            public IntPtr AgeGroup;
            public IntPtr Privileges;
            [MarshalAsAttribute(UnmanagedType.U1)]
            public bool IsSignedIn;
            public IntPtr WebAccountId;
            public IntPtr WindowsSystemUser;
        };
    }
}