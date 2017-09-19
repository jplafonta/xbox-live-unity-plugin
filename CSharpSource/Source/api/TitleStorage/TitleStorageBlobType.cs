// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.TitleStorage
#else
namespace Microsoft.Xbox.Services.TitleStorage
#endif
{
    /// <summary> Defines values used to indicate title storage blob type.</summary>
    public enum TitleStorageBlobType : int
    {
        /// <summary> Unknown blob type. </summary>
        Unknown,

        /// <summary>Binary blob type.</summary>
        Binary,

        /// <summary>JSON blob type.</summary>
        Json,

        /// <summary>Config blob type. </summary>
        Config
    }

}
