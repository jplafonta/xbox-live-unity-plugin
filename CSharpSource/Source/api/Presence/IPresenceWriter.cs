using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Presence
#else
namespace Microsoft.Xbox.Services.Presence
#endif
{
    public interface IPresenceWriter
    {
        void StartWriter();

        void StopWriter();

        void HandleTimeTrigger(object obj);

        void AddUser(XboxLiveUser xboxLiveUser);

    }
}
