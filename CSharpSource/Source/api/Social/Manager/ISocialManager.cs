// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Social.Manager
#else
namespace Microsoft.Xbox.Services.Social.Manager
#endif
{
    using global::System.Collections.Generic;
    using global::System.Threading.Tasks;

    public interface ISocialManager
    {
        IList<XboxLiveUser> LocalUsers { get; }

        Task AddLocalUser(XboxLiveUser user, SocialManagerExtraDetailLevel extraDetailLevel = SocialManagerExtraDetailLevel.None);

        void RemoveLocalUser(XboxLiveUser user);

        XboxSocialUserGroup CreateSocialUserGroupFromList(XboxLiveUser user, List<ulong> userIds);

        XboxSocialUserGroup CreateSocialUserGroupFromFilters(XboxLiveUser user, PresenceFilter presenceFilter, RelationshipFilter relationshipFilter);

        IList<SocialEvent> DoWork();
        
    }
}