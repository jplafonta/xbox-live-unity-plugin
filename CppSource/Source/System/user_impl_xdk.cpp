// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "user_impl_xdk.h"

using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Xbox::System;

FUNCTION_CONTEXT XSAPI_XBOX_LIVE_USER_IMPL::m_contextIndexer;
std::map<FUNCTION_CONTEXT, EventRegistrationToken> XSAPI_XBOX_LIVE_USER_IMPL::m_eventRegistrationTokens;

std::mutex XSAPI_XBOX_LIVE_USER_IMPL::m_mutex;
std::vector<XSAPI_XBOX_LIVE_USER*> XSAPI_XBOX_LIVE_USER_IMPL::m_users;

XSAPI_XBOX_LIVE_USER_IMPL::XSAPI_XBOX_LIVE_USER_IMPL(
    _In_ Windows::Xbox::System::User^ xboxSystemUser,
    _In_ XSAPI_XBOX_LIVE_USER *pUser
    )
    : m_pUser(pUser)
{
    Refresh();
}

FUNCTION_CONTEXT XSAPI_XBOX_LIVE_USER_IMPL::AddSignInCompletedHandler(
    _In_ XSAPI_SIGN_IN_COMPLETED_HANDLER signInHandler
    )
{
    FUNCTION_CONTEXT context = -1;
    if (signInHandler != nullptr)
    {
        auto token = Windows::Xbox::System::User::SignInCompleted += ref new EventHandler<SignInCompletedEventArgs^>(
            [signInHandler](Platform::Object^ sender, SignInCompletedEventArgs^ args)
        {
            std::lock_guard<std::mutex> lock(XSAPI_XBOX_LIVE_USER_IMPL::m_mutex);

            auto xboxSystemUser = args->User;
            auto iter = std::find_if(XSAPI_XBOX_LIVE_USER_IMPL::m_users.begin(),
                XSAPI_XBOX_LIVE_USER_IMPL::m_users.end(),
                [xboxSystemUser](XSAPI_XBOX_LIVE_USER* user)
            {
                return user->xboxSystemUser == xboxSystemUser;
            });

            if (iter != XSAPI_XBOX_LIVE_USER_IMPL::m_users.end())
            {
                signInHandler(*iter);
            }
        });

        {
            std::lock_guard<std::mutex> lock(m_mutex);
            m_eventRegistrationTokens[++m_contextIndexer] = token;
            context = m_contextIndexer;
        }
    }
    return context;
}

void XSAPI_XBOX_LIVE_USER_IMPL::RemoveSignInCompletedHandler(
    _In_ FUNCTION_CONTEXT context
)
{
    std::lock_guard<std::mutex> lock(m_mutex);
    auto iter = m_eventRegistrationTokens.find(context);
    if (iter != m_eventRegistrationTokens.end())
    {
        Windows::Xbox::System::User::SignInCompleted -= iter->second;
        m_eventRegistrationTokens.erase(iter);
    }
}

function_context XSAPI_XBOX_LIVE_USER_IMPL::AddSignOutCompletedHandler(
    _In_ XSAPI_SIGN_OUT_COMPLETED_HANDLER signOutHandler
)
{
    FUNCTION_CONTEXT context = -1;
    if (signOutHandler != nullptr)
    {
        auto token = Windows::Xbox::System::User::SignOutCompleted += ref new EventHandler<SignOutCompletedEventArgs^>(
            [signOutHandler](Platform::Object^ sender, SignOutCompletedEventArgs^ args)
        {
            std::lock_guard<std::mutex> lock(XSAPI_XBOX_LIVE_USER_IMPL::m_mutex);

            auto xboxSystemUser = args->User;
            auto iter = std::find_if(XSAPI_XBOX_LIVE_USER_IMPL::m_users.begin(),
                XSAPI_XBOX_LIVE_USER_IMPL::m_users.end(),
                [xboxSystemUser](XSAPI_XBOX_LIVE_USER* user)
            {
                return user->xboxSystemUser == xboxSystemUser;
            });

            if (iter != XSAPI_XBOX_LIVE_USER_IMPL::m_users.end())
            {
                signOutHandler(*iter);
            }
        });

        {
            std::lock_guard<std::mutex> lock(m_mutex);
            m_eventRegistrationTokens[++m_contextIndexer] = token;
            context = m_contextIndexer;
        }
    }
    return context;
}

void XSAPI_XBOX_LIVE_USER_IMPL::RemoveSignOutCompletedHandler(
    _In_ function_context context
    )
{
    std::lock_guard<std::mutex> lock(m_mutex);
    auto iter = m_eventRegistrationTokens.find(context);
    if (iter != m_eventRegistrationTokens.end())
    {
        Windows::Xbox::System::User::SignOutCompleted -= iter->second;
    }
}

void XSAPI_XBOX_LIVE_USER_IMPL::Refresh()
{
    if (m_pUser != nullptr && m_pUser->xboxSystemUser != nullptr)
    {
        m_xboxUserId = utils::to_utf8string(m_pUser->xboxSystemUser->XboxUserId->Data());
        m_pUser->xboxUserId = m_xboxUserId.data();

        m_gamertag = utils::to_utf8string(m_pUser->xboxSystemUser->DisplayInfo->Gamertag->Data());
        m_pUser->gamertag = m_gamertag.data();

        //m_cUser->ageGroup = m_xboxSystemUser->DisplayInfo->AgeGroup TODO convert this
        //m_cUser->privileges = m_xboxSystemUser->DisplayInfo->Privileges TODO

        m_pUser->isSignedIn = m_pUser->xboxSystemUser->IsSignedIn;
    }
}