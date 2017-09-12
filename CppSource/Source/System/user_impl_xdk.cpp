// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "user_impl_xdk.h"

using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Xbox::System;

std::mutex XboxLiveUserImpl::m_mutex;
std::vector<XboxLiveUser*> XboxLiveUserImpl::m_users;

XboxLiveUserImpl::XboxLiveUserImpl(
    _In_ Windows::Xbox::System::User^ xboxSystemUser,
    _In_ XboxLiveUser *cUser
    )
    : m_xboxSystemUser(xboxSystemUser),
    m_cUser(cUser)
{
    // TODO Refresh()
}

std::vector<XboxLiveUser*>& XboxLiveUserImpl::CreateUsersForXboxSystemUsers()
{
    // TODO Fix memory management of these user objects
    std::lock_guard<std::mutex> lock(XboxLiveUserImpl::m_mutex);
    m_users.clear();

    for (auto iter = Windows::Xbox::System::User::Users->First(); iter->HasCurrent; iter->MoveNext())
    {
        auto cUser = new XboxLiveUser();
        cUser->pImpl = new XboxLiveUserImpl(iter->Current, cUser);
        m_users.push_back(cUser);
    }
    return m_users;
}

function_context XboxLiveUserImpl::AddSignInCompletedHandler(
    _In_ SignInCompletedHandler signInHandler
    )
{
    // TODO fix function context vs handler token value type mismatch
    function_context context = -1;
    if (signInHandler != nullptr)
    {
        auto token = Windows::Xbox::System::User::SignInCompleted += ref new EventHandler<SignInCompletedEventArgs^>(
            [signInHandler](Platform::Object^ sender, SignInCompletedEventArgs^ args)
        {
            std::lock_guard<std::mutex> lock(XboxLiveUserImpl::m_mutex);

            auto xboxSystemUser = args->User;
            auto iter = std::find_if(XboxLiveUserImpl::m_users.begin(), 
                XboxLiveUserImpl::m_users.end(),
                [xboxSystemUser](XboxLiveUser* user)
            {
                return user->pImpl->m_xboxSystemUser == xboxSystemUser;
            });

            if (iter != XboxLiveUserImpl::m_users.end())
            {
                signInHandler(*iter);
            }
        });
        context = token.Value;
    }
    return context;
}

void XboxLiveUserImpl::RemoveSignInCompletedHandler(
    _In_ function_context context
    )
{   
    EventRegistrationToken token;
    token.Value = context;
    Windows::Xbox::System::User::SignInCompleted -= token;
}

function_context XboxLiveUserImpl::AddSignOutCompletedHandler(
    _In_ SignOutCompletedHandler signOutHandler
)
{
    function_context context = -1;
    if (signOutHandler != nullptr)
    {
        auto token = Windows::Xbox::System::User::SignOutCompleted += ref new EventHandler<SignOutCompletedEventArgs^>(
            [signOutHandler](Platform::Object^ sender, SignOutCompletedEventArgs^ args)
        {
            std::lock_guard<std::mutex> lock(XboxLiveUserImpl::m_mutex);

            auto xboxSystemUser = args->User;
            auto iter = std::find_if(XboxLiveUserImpl::m_users.begin(),
                XboxLiveUserImpl::m_users.end(),
                [xboxSystemUser](XboxLiveUser* user)
            {
                return user->pImpl->m_xboxSystemUser == xboxSystemUser;
            });

            if (iter != XboxLiveUserImpl::m_users.end())
            {
                signOutHandler(*iter);
            }
        });
        context = token.Value;
    }
    return context;
}

void XboxLiveUserImpl::RemoveSignOutCompletedHandler(
    _In_ function_context context
    )
{
    EventRegistrationToken token;
    token.Value = context;
    Windows::Xbox::System::User::SignOutCompleted -= token;
}