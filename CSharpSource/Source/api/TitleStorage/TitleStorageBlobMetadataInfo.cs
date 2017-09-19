﻿#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.TitleStorage
#else
namespace Microsoft.Xbox.Services.TitleStorage
#endif
{
    using global::System;

    public class TitleStorageBlobMetadataInfo
    {
        /// <summary>
        /// Name of the file
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// File Time set by the client
        /// </summary>
        public DateTimeOffset ClientFileTime { get; set; }
        
        /// <summary>
        /// Friendly name to be displayed in the UI
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// The Size of the Blob
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// ETag set on the blob
        /// </summary>
        public string ETag { get; set; }
        
        /// <summary>
        /// Type of the Blob. Possible Values are: config, json and binary.
        /// </summary>
        public TitleStorageBlobType BlobType { get; set; }
    }
}
