// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"

#if !(XDK_API | XBOX_UWP)
#include "user_impl_c.h"
#endif
#include "xbox_live_context_impl.h"

using namespace xbox::services;

#if XDK_API | XBOX_UWP

XSAPI_DLLEXPORT XboxLiveContext* XBL_CALLING_CONV
XboxLiveContextCreate(
    Windows::Xbox::System::User^ user
    )
{
    VerifyGlobalXsapiInit();
    auto context = new XboxLiveContext();
    context->pImpl = new XboxLiveContextImpl(user, context);
    return context;    
}

#else

XSAPI_DLLEXPORT XboxLiveContext* XBL_CALLING_CONV
XboxLiveContextCreate(
    XboxLiveUser *user
    )
{
    VerifyGlobalXsapiInit();
    auto context = new XboxLiveContext();
    context->pImpl = new XboxLiveContextImpl(user, context);
    return context;
}

#endif

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveContextDelete(
    XboxLiveContext *context
    )
{
    delete context->pImpl;
    delete context;
}
