// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Social.Manager
#else
namespace Microsoft.Xbox.Services.Social.Manager
#endif
{
    using global::System;

    [Flags]
    public enum SocialManagerExtraDetailLevel
    {
        None = 0x0,
        TitleHistory = 0x1,
        PreferredColor = 0x2,
    }
}