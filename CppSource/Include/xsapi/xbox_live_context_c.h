// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once
#include "types_c.h"

#if defined(__cplusplus)
extern "C" {
#endif


struct XSAPI_XBOX_LIVE_USER;
struct XSAPI_XBOX_LIVE_APP_CONFIG;
struct XSAPI_XBOX_LIVE_CONTEXT_IMPL;

typedef struct XSAPI_XBOX_LIVE_CONTEXT
{
    PCSTR xboxUserId;
    XSAPI_XBOX_LIVE_USER* pUser;
    CONST XSAPI_XBOX_LIVE_APP_CONFIG* pAppConfig;
    XSAPI_XBOX_LIVE_CONTEXT_IMPL* pImpl;
} XSAPI_XBOX_LIVE_CONTEXT;

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveContextCreate(
    _In_ CONST XSAPI_XBOX_LIVE_USER* pUser,
    _Out_ CONST XSAPI_XBOX_LIVE_CONTEXT** ppContext
    );

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveContextDelete(
    XSAPI_XBOX_LIVE_CONTEXT* pContext
    );

#if defined(__cplusplus)
} // end extern "C"
#endif // defined(__cplusplus)