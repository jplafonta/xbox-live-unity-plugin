// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#if XDK_API
#include "user_impl_xdk.h"
#else
#include "user_impl.h"
#endif

using namespace xbox::services;
using namespace xbox::services::system;
using namespace xbox::httpclient;

#if XDK_API
XSAPI_DLLEXPORT XboxLiveUser* XBL_CALLING_CONV
XboxLiveUserCreate(
    _In_ void* xboxSystemUser
    )
{
    // TODO
    return (XboxLiveUser*)47;
}

#else
#if UWP_API
XSAPI_DLLEXPORT XboxLiveUser* XBL_CALLING_CONV
XboxLiveUserCreateFromSystemUser(
    _In_ Windows::System::User^ creationContext
    )
{
    VerifyGlobalXsapiInit();

    auto cUser = new XboxLiveUser();
    cUser->pImpl = new XboxLiveUserImpl(creationContext, cUser);
    return cUser;
}
#endif // UWP_API

XSAPI_DLLEXPORT XboxLiveUser* XBL_CALLING_CONV
XboxLiveUserCreate()
{
    return XboxLiveUserCreateFromSystemUser(nullptr);
}
#endif // XDK_API

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserDelete(
    _In_ XboxLiveUser *user
    )
{
#if !XDK_API
    auto singleton = get_xsapi_singleton();
    std::lock_guard<std::mutex> lock(singleton->m_usersLock);
    
    singleton->m_signedInUsers.erase(user->pImpl->m_xboxUserId);
#endif
    delete user->pImpl;
    delete user;
}

#if !XDK_API

void XboxLiveUserSignInExecute(
    _In_opt_ void* context,
    _In_ HC_TASK_HANDLE taskHandle
    )
{
    xbox_live_result<sign_in_result> result;
    auto args = reinterpret_cast<xbl_args_xbox_live_user_sign_in*>(context);
        
    if (args->coreDispatcher == nullptr)
    {
        if (args->signInSilently)
        {
            result = args->user->pImpl->m_cppUser->signin_silently().get();
        }
        else
        {
            result = args->user->pImpl->m_cppUser->signin().get();
        }
    }
    else
    {
        if (args->signInSilently)
        {
            result = args->user->pImpl->m_cppUser->signin_silently(args->coreDispatcher).get();
        }
        else
        {
            result = args->user->pImpl->m_cppUser->signin(args->coreDispatcher).get();
        }
    }

    args->resultErrorMsg = to_wstring(result.err_message());
    args->result.result.errorCode = result.err().value();
    args->result.result.errorMessage = args->resultErrorMsg.c_str();
    
    if (!result.err())
    {
        args->result.payload.status = static_cast<SIGN_IN_STATUS>(result.payload().status());
        args->user->pImpl->Refresh();

        {
            auto singleton = get_xsapi_singleton();
            std::lock_guard<std::mutex> lock(singleton->m_usersLock);
            singleton->m_signedInUsers[args->user->pImpl->m_xboxUserId] = args->user;
        }
    }

    HCTaskSetCompleted(taskHandle);
}

void XboxLiveUserSignInHelper(
    _Inout_ XboxLiveUser* user,
    _In_ Platform::Object^ coreDispatcher,
    _In_ bool signInSilently,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    )
{
    auto args = new xbl_args_xbox_live_user_sign_in(user, coreDispatcher, signInSilently);

    HC_TASK_HANDLE taskHandle = HCTaskCreate(
        taskGroupId,
        XboxLiveUserSignInExecute,
        static_cast<void*>(args),
        xbl_execute_callback_fn<xbl_args_xbox_live_user_sign_in, SignInCompletionRoutine>,
        static_cast<void*>(args),
        static_cast<void*>(completionRoutine),
        completionRoutineContext,
        true
    );
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignIn(
    _Inout_ XboxLiveUser* user,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    )
{
    XboxLiveUserSignInHelper(user, nullptr, false, completionRoutine, completionRoutineContext, taskGroupId);
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignInSilently(
    _Inout_ XboxLiveUser* user,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    )
{
    XboxLiveUserSignInHelper(user, nullptr, true, completionRoutine, completionRoutineContext, taskGroupId);
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignInWithCoreDispatcher(
    _Inout_ XboxLiveUser* user,
    _In_ Platform::Object^ coreDispatcher,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    )
{
    XboxLiveUserSignInHelper(user, coreDispatcher, false, completionRoutine, completionRoutineContext, taskGroupId);
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignInSilentlyWithCoreDispatcher(
    _Inout_ XboxLiveUser* user,
    _In_ Platform::Object^ coreDispatcher,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
)
{
    XboxLiveUserSignInHelper(user, coreDispatcher, true, completionRoutine, completionRoutineContext, taskGroupId);
}

void XboxLiveUserGetTokenAndSignatureExecute(
    _In_opt_ void* context,
    _In_ HC_TASK_HANDLE taskHandle
    )
{
    auto args = reinterpret_cast<xbl_args_xbox_live_user_get_token_and_signature*>(context);    

    auto result = args->user->pImpl->m_cppUser->get_token_and_signature(
        args->httpMethod,
        args->url,
        args->headers,
        args->requestBodyString == nullptr ? string_t() : nullptr
        ).get();

    args->resultErrorMsg = to_wstring(result.err_message());
    args->result.result.errorCode = result.err().value();
    args->result.result.errorMessage = args->resultErrorMsg.c_str();

    if (!result.err())
    {
        auto cppPayload = result.payload();
        TokenAndSignatureResultPayload& payload = args->result.payload;
        
        args->token = cppPayload.token();
        payload.token = args->token.data();

        args->signature = cppPayload.signature();
        payload.signature = args->signature.data();

        args->xboxUserId = cppPayload.xbox_user_id();
        payload.xboxUserId = args->xboxUserId.data();

        args->gamertag = cppPayload.gamertag();
        payload.gamertag = args->gamertag.data();

        args->xboxUserHash = cppPayload.xbox_user_hash();
        payload.xboxUserHash = args->xboxUserHash.data();

        args->ageGroup = cppPayload.age_group();
        payload.ageGroup = args->ageGroup.data();

        args->privileges = cppPayload.privileges();
        payload.privileges = args->privileges.data();

        args->webAccountId = cppPayload.web_account_id();
        payload.webAccountId = args->webAccountId.data();
    }

    HCTaskSetCompleted(taskHandle);
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserGetTokenAndSignature(
    _Inout_ XboxLiveUser* user,
    _In_ PCSTR_T httpMethod,
    _In_ PCSTR_T url,
    _In_ PCSTR_T headers,
    _In_ PCSTR_T requestBodyString,
    _In_ GetTokenAndSignatureCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    )
{
    auto args = new xbl_args_xbox_live_user_get_token_and_signature(
        user,
        httpMethod,
        url,
        headers,
        requestBodyString);
    
    HC_TASK_HANDLE taskHandle = HCTaskCreate(
        taskGroupId,
        XboxLiveUserGetTokenAndSignatureExecute, 
        static_cast<void*>(args),
        xbl_execute_callback_fn<xbl_args_xbox_live_user_get_token_and_signature, GetTokenAndSignatureCompletionRoutine>, 
        static_cast<void*>(args),
        static_cast<void*>(completionRoutine), 
        completionRoutineContext,
        true
        );
}

#else // !XDK_API

XSAPI_DLLEXPORT function_context XBL_CALLING_CONV
AddSignInCompletedHandler(
    _In_ SignInCompletedHandler signInHandler
    )
{
    return XboxLiveUserImpl::AddSignInCompletedHandler(signInHandler);
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
RemoveSignInCompletedHandler(
    _In_ function_context context
    )
{
    XboxLiveUserImpl::RemoveSignInCompletedHandler(context);
}
#endif // !XDK_API

XSAPI_DLLEXPORT function_context XBL_CALLING_CONV
AddSignOutCompletedHandler(
    _In_ SignOutCompletedHandler signOutHandler
    )
{
    VerifyGlobalXsapiInit();
    return XboxLiveUserImpl::AddSignOutCompletedHandler(signOutHandler);
}

XSAPI_DLLEXPORT void XBL_CALLING_CONV
RemoveSignOutCompletedHandler(
    _In_ function_context context
    )
{
    VerifyGlobalXsapiInit();
    XboxLiveUserImpl::RemoveSignOutCompletedHandler(context);
}