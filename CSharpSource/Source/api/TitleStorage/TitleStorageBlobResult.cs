// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.TitleStorage
#else
namespace Microsoft.Xbox.Services.TitleStorage
#endif
{
    using global::System.Runtime.InteropServices;
    using global::System;

    /// <summary>
    /// Blob data returned from the cloud.
    /// </summary>
    public class TitleStorageBlobResult
    {
        /// <summary>
        /// Updated TitleStorageBlobMetadata object following an upload or download.
        /// </summary>
        public TitleStorageBlobMetadata BlobMetadata { get; private set; }

        /// <summary>
        /// The contents of the title storage blob.
        /// </summary>
        public byte[] BlobBuffer { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TitleStorageBlobResult"/> class.
        /// </summary>
        /// <param name="blobMetadata">Updated TitleStorageBlobMetadata object following an upload or download.</param>
        /// <param name="blobBuffer">The contents of the title storage blob.</param>
        public TitleStorageBlobResult(TitleStorageBlobMetadata blobMetadata, byte[] blobBuffer)
        {
            this.BlobMetadata = blobMetadata;
            this.BlobBuffer = blobBuffer;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct XSAPI_TITLE_STORAGE_BLOB_RESULT
    {
        public IntPtr blobBuffer;
        public UInt64 cbBlobBuffer;
        public IntPtr blobMetadata;
    }
}
