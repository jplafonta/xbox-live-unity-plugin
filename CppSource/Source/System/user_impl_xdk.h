// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once

struct XboxLiveUserImpl
{
public:   
    static std::vector<XboxLiveUser*>& CreateUsersForXboxSystemUsers();
        
    static function_context AddSignInCompletedHandler(_In_ SignInCompletedHandler signInHandler);
    static void RemoveSignInCompletedHandler(_In_ function_context context);
    
    static function_context AddSignOutCompletedHandler(_In_ SignOutCompletedHandler signOutHandler);
    static void RemoveSignOutCompletedHandler(_In_ function_context context);

private:
    XboxLiveUserImpl(
        _In_ Windows::Xbox::System::User^ xboxSystemUser,
        _In_ XboxLiveUser *cUser
        );

    //void Refresh();

    // TODO might not need these
    string_t m_xboxUserId;
    string_t m_gamertag;
    string_t m_ageGroup;
    string_t m_privileges;
    string_t m_webAccountId;

    XboxLiveUser* m_cUser;
    Windows::Xbox::System::User^ m_xboxSystemUser;

    static std::mutex m_mutex;
    static std::vector<XboxLiveUser*> m_users;
};