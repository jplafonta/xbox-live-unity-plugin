// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "xsapi/system_c.h"
#include "xsapi/xbox_live_context_c.h"
#include "xsapi/xbox_live_app_config_c.h"
#include "xbox_live_context_impl.h"
#if UWP_API
#include "user_impl_uwp.h"
#endif

XSAPI_XBOX_LIVE_CONTEXT_IMPL::XSAPI_XBOX_LIVE_CONTEXT_IMPL(_In_ CONST XSAPI_XBOX_LIVE_USER *pUser, _In_ XSAPI_XBOX_LIVE_CONTEXT* pContext)
    : m_pContext(pContext),
#if XDK_API
    m_cppContext(pUser->xboxSystemUser)
#else
    m_cppContext(pUser->pImpl->cppUser())
#endif
{
    GetXboxLiveAppConfigSingleton(&(pContext->pAppConfig));
}

xbox::services::xbox_live_context& XSAPI_XBOX_LIVE_CONTEXT_IMPL::cppObject()
{
    return m_cppContext;
}