// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public enum PresenceDeviceType
    {
        Unknown = 0,
        WindowsPhone,
        WindowsPhone7,
        Web,
        Xbox360,
        PC,
        Windows8,
        XboxOne,
        WindowsOneCore,
        WindowsOneCoreMobile,
        Android,
        iOS,
    }
}