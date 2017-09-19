// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Social
#else
namespace Microsoft.Xbox.Services.Social
#endif
{
    public class SocialService
    {

        //public event EventHandler<SocialRelationshipChangeEventArgs> SocialRelationshipChanged;


        public Task<XboxSocialRelationshipResult> GetSocialRelationshipsAsync(SocialRelationship socialRelationshipFilter, uint startIndex, uint maxItems)
        {
            throw new NotImplementedException();
        }

        public Task<XboxSocialRelationshipResult> GetSocialRelationshipsAsync(string xboxUserId)
        {
            throw new NotImplementedException();
        }

        public Task<XboxSocialRelationshipResult> GetSocialRelationshipsAsync(SocialRelationship socialRelationshipFilter)
        {
            throw new NotImplementedException();
        }

        public Task<XboxSocialRelationshipResult> GetSocialRelationshipsAsync()
        {
            throw new NotImplementedException();
        }

        public SocialRelationshipChangeSubscription SubscribeToSocialRelationshipChange(string xboxUserId)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeFromSocialRelationshipChange(SocialRelationshipChangeSubscription subscription)
        {
            throw new NotImplementedException();
        }

    }
}
