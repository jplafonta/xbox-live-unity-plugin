#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.TitleStorage
#else
namespace Microsoft.Xbox.Services.TitleStorage
#endif
{
    using global::System.Collections.Generic;

    /// <summary>
    /// Information used to retrieve metadata about Title Storage Blobs
    /// </summary>
    public class TitleStorageBlobMetadataResultInfo
    {
        /// <summary>
        /// List of Title Storage Blob Metadata
        /// </summary>
        public List<TitleStorageBlobMetadata> Blobs { get; set; }
        
        /// <summary>
        /// Paging Info
        /// </summary>
        public PagingInfo PagingInfo { get; set; }
    }

    public class PagingInfo
    {
        /// <summary>
        /// Continuation Token if available to retrieve next set of objects
        /// </summary>
        public string ContinuationToken { get; set; }
        
        /// <summary>
        /// Total number of blobs available
        /// </summary>
        public uint TotalItems { get; set; }
    }
}
