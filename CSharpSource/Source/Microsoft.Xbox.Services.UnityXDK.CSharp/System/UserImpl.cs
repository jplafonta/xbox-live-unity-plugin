// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Xbox.Services.System
{
    using global::System;
    using global::System.Runtime.InteropServices;
    using global::System.Threading.Tasks;

    internal class UserImpl : IUserImpl
    {
        public bool IsSignedIn { get; set; }
        public XboxLiveUser User { get; set; }

        public string XboxUserId { get; set; }
        public string Gamertag { get; set; }
        public string AgeGroup { get; set; }
        public string Privileges { get; set; }
        public string WebAccountId { get; set; }
        public AuthConfig AuthConfig { get; set; }

        public IntPtr m_xboxLiveUser_c;
        private Windows.Xbox.System.User m_xboxSystemUser;

        public UserImpl(Windows.Xbox.System.User xboxSystemUser)
        {
            m_xboxLiveUser_c = XboxLive.Instance.Invoke<IntPtr, XboxLiveUserCreate>(null);
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
                    // TODO populate other fields
                    Token = getTokenAndSignatureResult.Token,
                    Signature = getTokenAndSignatureResult.Signature
                };
            });
        }

        private delegate IntPtr XboxLiveUserCreate(IntPtr xboxSystemUser);

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