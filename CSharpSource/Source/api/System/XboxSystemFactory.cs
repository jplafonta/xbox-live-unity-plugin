// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.System
#else
namespace Microsoft.Xbox.Services.System
#endif
{
    using Microsoft.Xbox.Services.Shared;

    class XboxSystemFactory
    {
        private static XboxSystemFactory factory;
        public static XboxSystemFactory GetSingletonInstance()
        {
            if(factory == null)
            {
                factory = new XboxSystemFactory();
            }

            return factory;
        }

        private XboxSystemFactory()
        {
        }

        public IXboxWebsocketClient CreateWebsocketClient()
        {
#if WINDOWS_UWP
            return null;
#else
            return null;
#endif
        }
    }
}
