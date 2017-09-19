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
            get { return m_xboxSystemUser.IsSignedIn; }
        }
        public string XboxUserId
        {
            get { return m_xboxSystemUser.XboxUserId; }
        }
        public string Gamertag
        {
            get { return m_xboxSystemUser.DisplayInfo.Gamertag; }
        }
        public string AgeGroup
        {
            get { return m_xboxSystemUser.DisplayInfo.AgeGroup.ToString(); }
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

        private IntPtr m_xboxLiveUser_c;
        private Windows.Xbox.System.User m_xboxSystemUser;

        public UserImpl(Windows.Xbox.System.User xboxSystemUser)
        {
            if (xboxSystemUser == null)
            {
                throw new ArgumentException("XboxSystemUser cannot be null!");
            }
            m_xboxSystemUser = xboxSystemUser;
            m_xboxLiveUser_c = XboxLiveUserCreate(Marshal.GetIUnknownForObject(xboxSystemUser));
        }

        public Task<SignInResult> SignInImpl(bool showUI, bool forceRefresh)
        {
            throw new NotImplementedException();
        }

        public Task<TokenAndSignatureResult> InternalGetTokenAndSignatureAsync(string httpMethod, string url, string headers, byte[] body, bool promptForCredentialsIfNeeded, bool forceRefresh)
        {
            return Task.Run(() =>
            {
                var getTokenAndSignatureResult = m_xboxSystemUser.GetTokenAndSignatureAsync(httpMethod, url, headers, body).GetResults();

                return new TokenAndSignatureResult
                {
                    Token = getTokenAndSignatureResult.Token,
                    Signature = getTokenAndSignatureResult.Signature
                };
            });
        }

        [DllImport(XboxLive.FlatCDllName)]
        public static extern IntPtr XboxLiveUserCreate(IntPtr xboxSystemUser);

        [StructLayout(LayoutKind.Sequential)]
        private struct XboxLiveUser_c
        {
            [MarshalAsAttribute(UnmanagedType.LPWStr)]
            public string XboxUserId;

            [MarshalAsAttribute(UnmanagedType.LPWStr)]
            public string Gamertag;

            [MarshalAsAttribute(UnmanagedType.LPWStr)]
            public string AgeGroup;

            [MarshalAsAttribute(UnmanagedType.LPWStr)]
            public string Privileges;

            [MarshalAsAttribute(UnmanagedType.U1)]
            public byte IsSignedIn;

            [MarshalAsAttribute(UnmanagedType.SysInt)]
            public IntPtr XboxSystemUser;
        };
    }
}