// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "user_impl_uwp.h"

using namespace xbox::services::system;

XSAPI_XBOX_LIVE_USER_IMPL::XSAPI_XBOX_LIVE_USER_IMPL(
    _In_ Windows::System::User^ creationContext,
    _In_ XSAPI_XBOX_LIVE_USER* pUser
    )
    : m_pUser(pUser)
{
    if (creationContext != nullptr)
    {
        m_cppUser = std::make_shared<xbox::services::system::xbox_live_user>(creationContext);
    }
    else
    {
        m_cppUser = std::make_shared<xbox::services::system::xbox_live_user>();
    }
}

FUNCTION_CONTEXT XSAPI_XBOX_LIVE_USER_IMPL::AddSignOutCompletedHandler(
    _In_ XSAPI_SIGN_OUT_COMPLETED_HANDLER signOutHandler
    ) 
{
    return xbox_live_user::add_sign_out_completed_handler(
        [signOutHandler](const xbox::services::system::sign_out_completed_event_args& args)
    {
        auto singleton = get_xsapi_singleton();
        std::lock_guard<std::mutex> lock(singleton->m_usersLock);

        auto iter = singleton->m_signedInUsers.find(utils::to_utf8string(args.user()->xbox_user_id()));
        if (iter != singleton->m_signedInUsers.end())
        {
            iter->second->pImpl->Refresh();
            signOutHandler(iter->second);
        }
    });
}

void XSAPI_XBOX_LIVE_USER_IMPL::RemoveSignOutCompletedHandler(
    _In_ FUNCTION_CONTEXT context
    )
{
    xbox_live_user::remove_sign_out_completed_handler(context);
}

void XSAPI_XBOX_LIVE_USER_IMPL::Refresh()
{
    if (m_pUser != nullptr)
    {
        m_xboxUserId = utils::to_utf8string(m_cppUser->xbox_user_id());
        m_pUser->xboxUserId = m_xboxUserId.data();

        m_gamertag = utils::to_utf8string(m_cppUser->gamertag());
        m_pUser->gamertag = m_gamertag.data();

        m_ageGroup = utils::to_utf8string(m_cppUser->age_group());
        m_pUser->ageGroup = m_ageGroup.data();

        m_privileges = utils::to_utf8string(m_cppUser->privileges());
        m_pUser->privileges = m_privileges.data();

        m_pUser->isSignedIn = m_cppUser->is_signed_in();

#if WINAPI_FAMILY && WINAPI_FAMILY==WINAPI_FAMILY_APP
        m_webAccountId = utils::to_utf8string(m_cppUser->web_account_id());
        m_pUser->webAccountId = m_webAccountId.data();
#endif
        m_pUser->windowsSystemUser = m_cppUser->windows_system_user();
    }
}

std::shared_ptr<xbox::services::system::xbox_live_user> XSAPI_XBOX_LIVE_USER_IMPL::cppUser() const
{
    return m_cppUser;
}