// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    using global::System;

    public class ServiceCallLoggingConfig
    {
        public static ServiceCallLoggingConfig SingletonInstance { get; private set; }

        public void Enable()
        {
            throw new NotImplementedException();
        }

        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void RegisterForProtocolActivation()
        {
            throw new NotImplementedException();
        }
    }
}