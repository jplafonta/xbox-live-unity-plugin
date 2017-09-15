// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Xbox.Services
{
    using global::System;
    using Microsoft.Xbox.Services.System;

    public partial class XboxLiveUser
    {
        public XboxLiveUser(Windows.Xbox.System.User xboxSystemUser)
        {
            var init = XboxLive.Instance;
            this.userImpl = new UserImpl(xboxSystemUser);
        }
    }
}