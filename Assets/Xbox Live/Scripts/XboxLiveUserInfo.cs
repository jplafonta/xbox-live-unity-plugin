// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Collections.Generic;
using System.IO;

#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
using Plugin.Microsoft.Xbox.Services;
#else
using Microsoft.Xbox.Services;
#endif

using UnityEngine;

#if ENABLE_WINMD_SUPPORT
#if UNITY_XBOXONE
using Windows.Xbox.System;
#else
using Windows.System;
#endif
#endif

public class XboxLiveUserInfo : MonoBehaviour
{
    public XboxLiveUser User { get; private set; }

#if ENABLE_WINMD_SUPPORT
#if UNITY_XBOXONE
    public Windows.Xbox.System.User WindowsXboxSystemUser { get; set; }
#else
    public Windows.System.User WindowsSystemUser { get; set; }
#endif
#endif

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
    public void Start()
    {
        // Super simple check to determine if configuration is non-empty.  This is not a thorough check to determine if the configuration is valid.
        // A user can easly bypass this check which will just cause them to fail at runtime if they try to use any functionality.
        if (XboxLive.Instance.AppConfig == null || XboxLive.Instance.AppConfig.TitleId == 0 && Application.isPlaying)
        {
            const string message = "Xbox Live is not configured, but the game is attempting to use Xbox Live functionality.  You must update the configuration in 'Xbox Live > Configuration' before building the game to enable Xbox Live.";
            if (Application.isEditor && XboxLiveServicesSettings.Instance.DebugLogsOn)
            {
                Debug.LogWarning(message);
            }
            else
            {
                if (XboxLiveServicesSettings.Instance.DebugLogsOn)
                {
                    Debug.LogError(message);
                }
            }
        }

        MockXboxLiveData.Load(Path.Combine(Application.dataPath, "MockData.json"));
    }

    public void Initialize()
    {
#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
        // Require there be a user for now
        if (this.WindowsXboxSystemUser != null)
        {
            this.User = new XboxLiveUser(this.WindowsXboxSystemUser);
        }
#elif ENABLE_WINMD_SUPPORT
        if (this.WindowsSystemUser != null)
        {
            this.User = new XboxLiveUser(this.WindowsSystemUser);
        }
        else
        {
            this.User = new XboxLiveUser();
        }
#else
        this.User = new XboxLiveUser();
#endif
    }
}
