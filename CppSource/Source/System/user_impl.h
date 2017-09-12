// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once

struct XboxLiveUserImpl
{
public:
    XboxLiveUserImpl(
        _In_ Windows::System::User^ creationContext,
        _In_ XboxLiveUser *cUser
        );

    static function_context AddSignOutCompletedHandler(_In_ SignOutCompletedHandler signOutHandler);
    static void RemoveSignOutCompletedHandler(_In_ function_context context);

    void Refresh();

    string_t m_xboxUserId;
    string_t m_gamertag;
    string_t m_ageGroup;
    string_t m_privileges;
    string_t m_webAccountId;

    XboxLiveUser* m_cUser;
    std::shared_ptr<xbox::services::system::xbox_live_user> m_cppUser;
};