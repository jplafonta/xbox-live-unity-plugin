// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once

#include "xsapi/system_c.h"

struct XSAPI_XBOX_LIVE_USER_IMPL
{
public: 
    XSAPI_XBOX_LIVE_USER_IMPL(_In_ Windows::Xbox::System::User^ xboxSystemUser, _In_ XSAPI_XBOX_LIVE_USER *pUser);

    static FUNCTION_CONTEXT AddSignInCompletedHandler(_In_ XSAPI_SIGN_IN_COMPLETED_HANDLER signInHandler);
    static void RemoveSignInCompletedHandler(_In_ FUNCTION_CONTEXT context);
    
    static FUNCTION_CONTEXT AddSignOutCompletedHandler(_In_ XSAPI_SIGN_OUT_COMPLETED_HANDLER signOutHandler);
    static void RemoveSignOutCompletedHandler(_In_ FUNCTION_CONTEXT context);

    void Refresh();

private:
    std::string m_xboxUserId;
    std::string m_gamertag;
    std::string m_ageGroup;
    std::string m_privileges;

    XSAPI_XBOX_LIVE_USER* m_pUser;

    static std::vector<XSAPI_XBOX_LIVE_USER*> m_users;

    static FUNCTION_CONTEXT m_contextIndexer;
    static std::map<FUNCTION_CONTEXT, Windows::Foundation::EventRegistrationToken> m_eventRegistrationTokens;

    static std::mutex m_mutex;
};