// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;

#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
using Plugin.Microsoft.Xbox.Services;
#else
using Microsoft.Xbox.Services;
#endif

public class XboxLiveUserEventArgs : EventArgs
{
    public XboxLiveUserEventArgs(XboxLiveUser user)
    {
        this.User = user;
    }

    public XboxLiveUser User { get; private set; }
}