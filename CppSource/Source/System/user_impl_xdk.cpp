// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "user_impl_xdk.h"

using namespace Windows::Foundation;
using namespace Windows::Foundation::Collections;
using namespace Windows::Xbox::System;

function_context XboxLiveUserImpl::m_contextIndexer;
std::map<function_context, EventRegistrationToken> XboxLiveUserImpl::m_eventRegistrationTokens;

std::mutex XboxLiveUserImpl::m_mutex;
std::vector<XboxLiveUser*> XboxLiveUserImpl::m_users;

XboxLiveUserImpl::XboxLiveUserImpl(
    _In_ Windows::Xbox::System::User^ xboxSystemUser,
    _In_ XboxLiveUser *cUser
    )
    : m_xboxSystemUser(xboxSystemUser),
    m_cUser(cUser)
{
    Refresh();
}

function_context XboxLiveUserImpl::AddSignInCompletedHandler(
    _In_ SignInCompletedHandler signInHandler
    )
{
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

        {
            std::lock_guard<std::mutex> lock(m_mutex);
            m_eventRegistrationTokens[++m_contextIndexer] = token;
            context = m_contextIndexer;
        }
    }
    return context;
}

void XboxLiveUserImpl::RemoveSignInCompletedHandler(
    _In_ function_context context
)
{
    std::lock_guard<std::mutex> lock(m_mutex);
    auto iter = m_eventRegistrationTokens.find(context);
    if (iter != m_eventRegistrationTokens.end())
    {
        Windows::Xbox::System::User::SignInCompleted -= iter->second;
    }
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

        {
            std::lock_guard<std::mutex> lock(m_mutex);
            m_eventRegistrationTokens[++m_contextIndexer] = token;
            context = m_contextIndexer;
        }
    }
    return context;
}

void XboxLiveUserImpl::RemoveSignOutCompletedHandler(
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

void XboxLiveUserImpl::Refresh()
{
    if (m_xboxSystemUser != nullptr)
    {
        m_cUser->xboxUserId = m_xboxSystemUser->XboxUserId->Data();
        m_cUser->gamertag = m_xboxSystemUser->DisplayInfo->Gamertag->Data();
        //m_cUser->ageGroup = m_xboxSystemUser->DisplayInfo->AgeGroup TODO convert this
        //m_cUser->privileges = m_xboxSystemUser->DisplayInfo->Privileges TODO
        m_cUser->isSignedIn = m_xboxSystemUser->IsSignedIn;
    }
}