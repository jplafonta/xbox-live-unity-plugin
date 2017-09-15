// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once

struct XboxLiveUserImpl
{
public:   
    XboxLiveUserImpl(
        _In_ Windows::Xbox::System::User^ xboxSystemUser,
        _In_ XboxLiveUser *cUser
        );
        
    static function_context AddSignInCompletedHandler(_In_ SignInCompletedHandler signInHandler);
    static void RemoveSignInCompletedHandler(_In_ function_context context);
    
    static function_context AddSignOutCompletedHandler(_In_ SignOutCompletedHandler signOutHandler);
    static void RemoveSignOutCompletedHandler(_In_ function_context context);

private:
    void Refresh();

    XboxLiveUser* m_cUser;
    Windows::Xbox::System::User^ m_xboxSystemUser;

    static std::vector<XboxLiveUser*> m_users;
    
    static function_context m_contextIndexer;
    static std::map<function_context, Windows::Foundation::EventRegistrationToken> m_eventRegistrationTokens;

    static std::mutex m_mutex;
};