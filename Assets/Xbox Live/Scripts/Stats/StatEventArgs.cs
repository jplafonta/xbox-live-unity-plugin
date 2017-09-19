// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;

#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
using Plugin.Microsoft.Xbox.Services;
using Plugin.Microsoft.Xbox.Services.Statistics.Manager;
#else
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Statistics.Manager;
#endif

namespace XboxLivePrefab
{
    public class StatEventArgs : EventArgs
    {
        public StatEventArgs(StatEvent statEvent)
        {
            this.EventData = statEvent;
        }

        public StatEvent EventData { get; private set; }
    }
}