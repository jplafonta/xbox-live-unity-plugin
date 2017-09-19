// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Statistics.Manager
#else
namespace Microsoft.Xbox.Services.Statistics.Manager
#endif
{
    using global::System;

    public class StatEvent
    {
        public StatEventType EventType { get; private set; }
        public StatEventArgs EventArgs { get; private set; }
        public XboxLiveUser LocalUser { get; private set; }
        public Exception ErrorInfo { get; private set; }

        public StatEvent(StatEventType eventType, XboxLiveUser user, Exception errorInfo, StatEventArgs args)
        {
            this.EventType = eventType;
            this.LocalUser = user;
            this.ErrorInfo = errorInfo;
            this.EventArgs = args;
        }
    }
}