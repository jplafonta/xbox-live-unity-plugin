// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Privacy.Models
#else
namespace Microsoft.Xbox.Services.Privacy.Models
#endif
{
    internal class PrivacySettingsRequest
    {
        public PrivacySettingsRequest(IList<string> permissionIds, IList<string> targetXboxUserIds)
        {
            if (permissionIds == null)
            {
                throw new ArgumentNullException("permissionIds ");
            }

            if (targetXboxUserIds == null)
            {
                throw new ArgumentNullException("targetXboxUserIds");
            }

            this.Permissions = permissionIds;

            this.Users = new List<PrivacyUser>();
            foreach (var xuid in targetXboxUserIds)
            {
                var user = new PrivacyUser(xuid);
                this.Users.Add(user);
            }
        }

        public IList<string> Permissions { get; set; }

        public List<PrivacyUser> Users { get; set; }
    }

    internal class PrivacyUser
    {
        public PrivacyUser(string targetXboxUserId)
        {
            this.Xuid = targetXboxUserId;
        }

        public string Xuid { get; set; }
    }
}
