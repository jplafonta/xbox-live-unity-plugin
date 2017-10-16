// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once

#include "xsapi/system_c.h"

struct XSAPI_XBOX_LIVE_USER_IMPL
{
public:
    XSAPI_XBOX_LIVE_USER_IMPL(_In_ Windows::System::User^ creationContext, _In_ XSAPI_XBOX_LIVE_USER* pUser);

    static FUNCTION_CONTEXT AddSignOutCompletedHandler(_In_ XSAPI_SIGN_OUT_COMPLETED_HANDLER signOutHandler);
    static void RemoveSignOutCompletedHandler(_In_ FUNCTION_CONTEXT context);

    void Refresh();

    std::shared_ptr<xbox::services::system::xbox_live_user> cppUser() const;

private:
    std::string m_xboxUserId;
    std::string m_gamertag;
    std::string m_ageGroup;
    std::string m_privileges;
    std::string m_webAccountId;

    XSAPI_XBOX_LIVE_USER* m_pUser;
    std::shared_ptr<xbox::services::system::xbox_live_user> m_cppUser;
};