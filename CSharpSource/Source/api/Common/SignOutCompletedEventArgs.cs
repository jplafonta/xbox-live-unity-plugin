// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    using global::System;

    public class SignOutCompletedEventArgs : EventArgs
    {
        public SignOutCompletedEventArgs(IXboxLiveUser user)
        {
            this.User = user;
        }

        public IXboxLiveUser User { get; private set; }
    }
}