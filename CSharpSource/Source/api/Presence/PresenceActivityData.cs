using System;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public class PresenceActivityData
    {
        public PresenceData PresenceData { get; set; }

        public MediaPresenceData MediaPresenceData { get; set; }

        public bool ShouldSerialize { get; set; }

        public PresenceActivityData() {

        }

        public PresenceActivityData(PresenceData presenceData, MediaPresenceData mediaPresenceData ) {

        }

        public string Serialize() {
            throw new NotImplementedException();
        }
    }
}