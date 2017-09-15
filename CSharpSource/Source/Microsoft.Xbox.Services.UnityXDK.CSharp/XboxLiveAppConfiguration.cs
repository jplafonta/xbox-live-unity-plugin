// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

namespace Microsoft.Xbox.Services
{
    using global::System;

    public partial class XboxLiveAppConfiguration
    {
        public static XboxLiveAppConfiguration Load(string path)
        {
            return new XboxLiveAppConfiguration
            {
                Sandbox = Windows.Xbox.Services.XboxLiveConfiguration.SandboxId,
                PrimaryServiceConfigId = Windows.Xbox.Services.XboxLiveConfiguration.PrimaryServiceConfigId,
                TitleId = UInt32.Parse(Windows.Xbox.Services.XboxLiveConfiguration.TitleId)
            };
        }
    }
}