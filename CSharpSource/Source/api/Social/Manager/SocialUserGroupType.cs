// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Social.Manager
#else
namespace Microsoft.Xbox.Services.Social.Manager
#endif
{
    public enum SocialUserGroupType
    {
        Unknown = 0,
        Filter,
        UserList,
    }
}