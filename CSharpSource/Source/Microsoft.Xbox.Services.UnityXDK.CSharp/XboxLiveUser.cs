// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Xbox.Services
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Runtime.InteropServices;
    using Microsoft.Xbox.Services.System;
    using Windows.Xbox.System;

    public partial class XboxLiveUser
    {
        public IntPtr cUser { get; set; }

        public XboxLiveUser(Windows.Xbox.System.User xboxSystemUser)
        {
            var impl = new UserImpl(xboxSystemUser);
            this.cUser = impl.m_xboxLiveUser_c;
            this.userImpl = impl;
        }
    }
}