#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.TitleStorage
#else
namespace Microsoft.Xbox.Services.TitleStorage
#endif
{
    public class QuotaInfoResult
    {
        public TitleStorageQuota QuotaInfo { get; set; }
    }
}
