// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#if !(XDK_API | XBOX_UWP)
#include "user_impl_c.h"
#endif
#include "xbox_live_context_impl.h"

#if XDK_API | XBOX_UWP

XboxLiveContextImpl::XboxLiveContextImpl(
    _In_ Windows::Xbox::System::User^ user,
    _In_ XboxLiveContext *cContext
    )
    : m_cContext(cContext),
    m_cppContext(user)
{
    cContext->appConfig = GetXboxLiveAppConfigSingleton();
}

#else

XboxLiveContextImpl::XboxLiveContextImpl(
    _In_ XboxLiveUser *user,
    _In_ XboxLiveContext *cContext
)
    : m_cContext(cContext),
    m_cppContext(user->pImpl->m_cppUser)
{
    cContext->appConfig = GetXboxLiveAppConfigSingleton();
}

#endif