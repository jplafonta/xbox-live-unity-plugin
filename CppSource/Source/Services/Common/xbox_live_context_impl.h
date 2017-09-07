// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once

struct XboxLiveContextImpl
{
#if XDK_API | XBOX_UWP
    XboxLiveContextImpl(
        _In_ Windows::Xbox::System::User^ user,
        _In_ XboxLiveContext *cContext
        );
#else
    XboxLiveContextImpl(
        _In_ XboxLiveUser *user,
        _In_ XboxLiveContext *cContext
        );
#endif

    XboxLiveContext *m_cContext;
    xbox::services::xbox_live_context m_cppContext;
};