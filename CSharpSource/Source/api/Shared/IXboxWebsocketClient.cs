// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Shared
#else
namespace Microsoft.Xbox.Services.Shared
#endif
{
    using global::System;
    using global::System.Threading.Tasks;

    interface IXboxWebsocketClient : IDisposable
    {
        Task<object> Connect(
            XboxLiveUser xblUser,
            string uri,
            string subprotocol);

        void Send(string message, Action<bool> onSendComplete);

        void Close();
    }
}
