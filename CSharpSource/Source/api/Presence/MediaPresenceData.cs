using System;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public class MediaPresenceData
    {
        public string MediaId { get; set; }

        public PresenceMediaIdType MediaIdType { get; set; }

        public bool ShouldSerialize { get; set; }

        public string Serialize()
        {
            throw new NotImplementedException();
        }
    }
}