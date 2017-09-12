// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "user_impl.h"

using namespace xbox::services::system;

XboxLiveUserImpl::XboxLiveUserImpl(
    _In_ Windows::System::User^ creationContext,
    _In_ XboxLiveUser *cUser
    )
    : m_cUser(cUser)
{
    if (creationContext != nullptr)
    {
        m_cppUser = std::make_shared<xbox_live_user>(creationContext);
    }
    else
    {
        m_cppUser = std::make_shared<xbox_live_user>();
    }
}

function_context XboxLiveUserImpl::AddSignOutCompletedHandler(
    _In_ SignOutCompletedHandler signOutHandler
    )
{
    return xbox_live_user::add_sign_out_completed_handler(
        [signOutHandler](const xbox::services::system::sign_out_completed_event_args& args)
    {
        auto singleton = get_xsapi_singleton();
        std::lock_guard<std::mutex> lock(singleton->m_usersLock);

        auto iter = singleton->m_signedInUsers.find(args.user()->xbox_user_id());
        if (iter != singleton->m_signedInUsers.end())
        {
            iter->second->pImpl->Refresh();
            signOutHandler(iter->second);
        }
    });
}

void XboxLiveUserImpl::RemoveSignOutCompletedHandler(
    _In_ function_context context
    )
{
    xbox_live_user::remove_sign_out_completed_handler(context);
}

void XboxLiveUserImpl::Refresh()
{
    if (m_cUser != nullptr)
    {
        m_xboxUserId = m_cppUser->xbox_user_id();
        m_cUser->xboxUserId = m_xboxUserId.data();

        m_gamertag = m_cppUser->gamertag();
        m_cUser->gamertag = m_gamertag.data();

        m_ageGroup = m_cppUser->age_group();
        m_cUser->ageGroup = m_ageGroup.data();

        m_privileges = m_cppUser->privileges();
        m_cUser->privileges = m_privileges.data();

        m_cUser->isSignedIn = m_cppUser->is_signed_in();

#if UWP_API
        m_webAccountId = m_cppUser->web_account_id();
        m_cUser->webAccountId = m_webAccountId.data();
#endif
        m_cUser->windowsSystemUser = m_cppUser->windows_system_user();
    }
}