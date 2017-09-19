using System;
using System.Collections.Generic;
using System.Text;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public class UserPresenceWriterStatus
    {
        public bool ShouldRemove { get; set; }

        public float HeartBeatIntervalInMins { get; set; }
    }
}
