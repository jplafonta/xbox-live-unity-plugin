using System;
using System.Collections.Generic;
using System.Text;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public class PresenceTitleRequest
    {
        public bool IsUserActiveInTitle { get; set; }
        public PresenceData PresenceData { get; set; }

        public PresenceTitleRequest(bool isUserActiveInTitle, PresenceData presenceData)
        {
            this.IsUserActiveInTitle = isUserActiveInTitle;
            this.PresenceData = presenceData;
        }
    }
}
