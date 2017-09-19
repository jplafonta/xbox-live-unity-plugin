// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    using global::System;

    public class TitlePresenceChangeEventArgs : EventArgs
    {
        public TitlePresenceState TitleState { get; internal set; }

        public uint TitleId { get; internal set; }

        public string XboxUserId { get; internal set; }
    }
}