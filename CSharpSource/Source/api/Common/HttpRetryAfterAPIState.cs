// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    using global::System;

    public class HttpRetryAfterApiState
    {
        public DateTime RetryAfterTime { get; set; }
        public Exception Exception { get; set; }
        public XboxLiveHttpResponse HttpCallResponse { get; set; }
    }
}


