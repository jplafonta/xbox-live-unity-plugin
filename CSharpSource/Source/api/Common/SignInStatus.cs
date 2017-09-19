// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    public enum SignInStatus
    {
        Success = 0,
        UserInteractionRequired = 1,
        UserCancel = 2,
    }
}