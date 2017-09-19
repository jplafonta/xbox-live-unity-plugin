// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
using Windows.Xbox.System;
using Plugin.Microsoft.Xbox.Services;
using Plugin.Microsoft.Xbox.Services.Social.Manager;
#else
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Social.Manager;
#endif

using UnityEngine;
using UnityEngine.UI;

public class UserProfile : MonoBehaviour
{
    public XboxLiveUserInfo XboxLiveUser;

    public string InputControllerButton;

    private bool SignInCalledOnce;

    [HideInInspector]
    public GameObject signInPanel;

    [HideInInspector]
    public GameObject profileInfoPanel;

    [HideInInspector]
    public Image gamerpic;

    [HideInInspector]
    public Image gamerpicMask;

    [HideInInspector]
    public Text gamertag;

    [HideInInspector]
    public Text gamerscore;

    [HideInInspector]
    public XboxLiveUserInfo XboxLiveUserPrefab;

    public bool AllowGuestAccounts = false;

    public void Awake()
    {
        this.EnsureEventSystem();
        XboxLiveServicesSettings.EnsureXboxLiveServicesSettings();

        if (!XboxLiveUserManager.Instance.IsInitialized)
        {
            XboxLiveUserManager.Instance.Initialize();
        }

        XboxLive.Instance.PresenceWriter.StartWriter();

    }

    public void Start()
    {
#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE

        // There should be no sign in panel in the prefab for Xbox One since sign in is
        // handled by the system.
        //this.signInPanel.SetActive(false);
        //this.signInPanel.GetComponentInChildren<Button>().interactable = false;

        // TODO does this event handler get called correctly
        Windows.Xbox.System.User.UserAdded += (object sender, Windows.Xbox.System.UserAddedEventArgs args) =>
        {
            // TODO should we consider this user even if we already have one
            if (this.XboxLiveUser.WindowsXboxSystemUser == null)
            {
                if (!args.User.IsGuest && args.User.IsSignedIn)
                {
                    this.XboxLiveUser.WindowsXboxSystemUser = args.User;
                    //this.StartCoroutine(this.InitializeXboxLiveUserCoroutine());
                }
            }
        };
#else
        // Disable the sign-in button if there's no configuration available.
        if (XboxLive.Instance.AppConfig == null || XboxLive.Instance.AppConfig.AppId == null)
        {
            Button signInButton = this.signInPanel.GetComponentInChildren<Button>();
            signInButton.interactable = false;
            this.profileInfoPanel.SetActive(false);
            Text signInButtonText = signInButton.GetComponentInChildren<Text>(true);
            if (signInButtonText != null)
            {
                signInButtonText.fontSize = 16;
                signInButtonText.text = "Xbox Live is not enabled.\nSee errors for detail.";
            }
        }
#endif
        if (XboxLiveUserManager.Instance.SingleUserModeEnabled)
        {
            if (XboxLiveUserManager.Instance.UserForSingleUserMode == null)
            {
                XboxLiveUserManager.Instance.UserForSingleUserMode = Instantiate(this.XboxLiveUserPrefab);
                this.XboxLiveUser = XboxLiveUserManager.Instance.UserForSingleUserMode;
#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
                this.InitializeXboxLiveUser();
#else
                if (XboxLive.Instance.AppConfig != null && XboxLive.Instance.AppConfig.AppId != null)
                {
                    this.InitializeXboxLiveUser();
                }
#endif
            }
            else
            {
                this.XboxLiveUser = XboxLiveUserManager.Instance.UserForSingleUserMode;
                this.StartCoroutine(this.LoadProfileInfo());
            }
        }       

        this.Refresh();
    }

#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
    private void XboxLiveUserOnSignOutCompleted(object sender, Windows.Xbox.System.SignOutCompletedEventArgs signOutCompletedEventArgs)
    {
        this.Refresh();
    }
#else
    private void XboxLiveUserOnSignOutCompleted(object sender, SignOutCompletedEventArgs signOutCompletedEventArgs)
    {
        this.Refresh();
    }
#endif


    public void InitializeXboxLiveUser()
    {
#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
        // Look at currently active users and just use the first one if there is one
        var xboxSystemUsers = Windows.Xbox.System.User.Users;

        foreach (var xboxSystemUser in xboxSystemUsers)
        {
            if (!xboxSystemUser.IsGuest && xboxSystemUser.IsSignedIn)
            {
                this.XboxLiveUser.WindowsXboxSystemUser = xboxSystemUser;
                //this.StartCoroutine(this.InitializeXboxLiveUserCoroutine());
            }
        }
#else
        this.StartCoroutine(this.InitializeXboxLiveUserCoroutine());
#endif
    }

    public void Update()
    {
#if ENABLE_WINMD_SUPPORT && UNITY_XBOXONE
        // TODO finish
        if (this.XboxLiveUser != null && this.XboxLiveUser.User != null && this.XboxLiveUser.User.IsSignedIn && !this.SignInCalledOnce)
        {
            this.SignInCalledOnce = true;
            this.StartCoroutine(this.LoadProfileInfo());
        }

        if (this.XboxLiveUser != null && this.XboxLiveUser.WindowsXboxSystemUser != null && Input.GetKeyDown(this.InputControllerButton))
        {
            this.StartCoroutine(this.InitializeXboxLiveUserCoroutine());
        }
#else
        if (XboxLiveUserManager.Instance.SingleUserModeEnabled && XboxLiveUserManager.Instance.UserForSingleUserMode != null
            && XboxLiveUserManager.Instance.UserForSingleUserMode.User != null
            && !XboxLiveUserManager.Instance.UserForSingleUserMode.User.IsSignedIn && !this.SignInCalledOnce)
        {
            this.SignInCalledOnce = true;
            this.StartCoroutine(this.SignInAsync());
        }

        if (this.XboxLiveUser != null && this.XboxLiveUser.User != null && !this.XboxLiveUser.User.IsSignedIn && !this.SignInCalledOnce)
        {
            this.SignInCalledOnce = true;
            this.StartCoroutine(this.SignInAsync());
        }

        if (!this.SignInCalledOnce && !string.IsNullOrEmpty(this.InputControllerButton) && Input.GetKeyDown(this.InputControllerButton))
        {
            this.StartCoroutine(this.InitializeXboxLiveUserCoroutine());
        }
#endif
    }


    public IEnumerator InitializeXboxLiveUserCoroutine()
    {
        yield return null;

#if !UNITY_XBOXONE
        // Disable the sign-in button
        this.signInPanel.GetComponentInChildren<Button>().interactable = false;
#endif

#if UNITY_XBOXONE && ENABLE_WINMD_SUPPORT
        // TODO is this complete
        if (this.XboxLiveUser == null)
        {
            this.XboxLiveUser = XboxLiveUserManager.Instance.UserForSingleUserMode;
        }
        if (this.XboxLiveUser.User == null)
        {
            this.XboxLiveUser.Initialize();
        }
#elif ENABLE_WINMD_SUPPORT
        if (!XboxLiveUserManager.Instance.SingleUserModeEnabled && this.XboxLiveUser != null && this.XboxLiveUser.WindowsSystemUser == null)
        {
            var autoPicker = new Windows.System.UserPicker { AllowGuestAccounts = this.AllowGuestAccounts };
            autoPicker.PickSingleUserAsync().AsTask().ContinueWith(
                    task =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                this.XboxLiveUser.WindowsSystemUser = task.Result;
                                this.XboxLiveUser.Initialize();
                            }
                            else
                            {
                                if (XboxLiveServicesSettings.Instance.DebugLogsOn)
                                {
                                    Debug.Log("Exception occured: " + task.Exception.Message);
                                }
                            }
                        });
        }
        else
        {
            if (this.XboxLiveUser == null)
            {
                this.XboxLiveUser = XboxLiveUserManager.Instance.UserForSingleUserMode;
            }
            if (this.XboxLiveUser.User == null)
            {
                this.XboxLiveUser.Initialize();
            }
        }
#else
        if (XboxLiveUserManager.Instance.SingleUserModeEnabled && this.XboxLiveUser == null)
        {
            this.XboxLiveUser = XboxLiveUserManager.Instance.GetSingleModeUser();
        }

        this.XboxLiveUser.Initialize();
#endif
    }

#if !(UNITY_XBOXONE && ENABLE_WINMD_SUPPORT)
    public IEnumerator SignInAsync()
    {
        SignInStatus signInStatus;
        TaskYieldInstruction<SignInResult> signInSilentlyTask = this.XboxLiveUser.User.SignInSilentlyAsync().AsCoroutine();
        yield return signInSilentlyTask;

        signInStatus = signInSilentlyTask.Result.Status;
        if (signInSilentlyTask.Result.Status != SignInStatus.Success)
        {
            TaskYieldInstruction<SignInResult> signInTask = this.XboxLiveUser.User.SignInAsync().AsCoroutine();
            yield return signInTask;

            signInStatus = signInTask.Result.Status;
        }

        // Throw any exceptions if needed.
        if (signInStatus == SignInStatus.Success)
        {
            yield return this.LoadProfileInfo();
        }
    }
#endif //!UNITY_XBOXONE

    private IEnumerator LoadProfileInfo()
    {
        XboxLive.Instance.StatsManager.AddLocalUser(this.XboxLiveUser.User);
        XboxLive.Instance.PresenceWriter.AddUser(this.XboxLiveUser.User);
        var addLocalUserTask =
            XboxLive.Instance.SocialManager.AddLocalUser(
                this.XboxLiveUser.User,
                SocialManagerExtraDetailLevel.PreferredColor).AsCoroutine();
        yield return addLocalUserTask;

        var userId = ulong.Parse(this.XboxLiveUser.User.XboxUserId);
        var group = XboxLive.Instance.SocialManager.CreateSocialUserGroupFromList(this.XboxLiveUser.User, new List<ulong> { userId });
        var socialUser = group.GetUser(userId);

        var www = new WWW(socialUser.DisplayPicRaw + "&w=128");
        yield return null;

        try
        {
            if (www.isDone && string.IsNullOrEmpty(www.error))
            {
                var t = www.texture;
                var r = new Rect(0, 0, t.width, t.height);
                this.gamerpic.sprite = Sprite.Create(t, r, Vector2.zero);
            }

            this.gamertag.text = this.XboxLiveUser.User.Gamertag;
            this.gamerscore.text = socialUser.Gamerscore;

            if (socialUser.PreferredColor != null)
            {
                this.profileInfoPanel.GetComponent<Image>().color =
                    ColorFromHexString(socialUser.PreferredColor.PrimaryColor);
                this.gamerpicMask.color = ColorFromHexString(socialUser.PreferredColor.PrimaryColor);
            }

        }
        catch (Exception ex)
        {
            if (XboxLiveServicesSettings.Instance.DebugLogsOn)
            {
                Debug.Log("There was an error while loading Profile Info. Exception: " + ex.Message);
            }
        }

        this.Refresh();
    }

    public static Color ColorFromHexString(string color)
    {
        var r = (float)byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber) / 255;
        var g = (float)byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber) / 255;
        var b = (float)byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber) / 255;

        return new Color(r, g, b);
    }

    private void Refresh()
    {
        var isSignedIn = this.XboxLiveUser != null && this.XboxLiveUser.User != null && this.XboxLiveUser.User.IsSignedIn;
#if !UNITY_XBOXONE
        this.signInPanel.SetActive(!isSignedIn);
#endif
        this.profileInfoPanel.SetActive(isSignedIn);
    }

    private void OnApplicationQuit()
    {
        XboxLive.Instance.PresenceWriter.StopWriter();
    }
}