using System;
using System.Collections.Generic;
using System.Text;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public class SimplePresenceRequest
    {
        public string State { get; set; }
    }
}
