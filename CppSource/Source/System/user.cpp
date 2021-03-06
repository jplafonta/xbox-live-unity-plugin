// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#include "pch.h"
#include "user_impl.h"
#include "user_taskargs.h"

using namespace xbox::services;
using namespace xbox::services::system;

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserCreateFromSystemUser(
    _In_ Windows::System::User^ creationContext,
    _Out_ XSAPI_XBOX_LIVE_USER* *ppUser
    ) XSAPI_NOEXCEPT
try
{
    if (ppUser == nullptr)
    {
        return XSAPI_RESULT_E_HC_INVALIDARG;
    }

    auto cUser = new XSAPI_XBOX_LIVE_USER();
    cUser->pImpl = new XSAPI_XBOX_LIVE_USER_IMPL(creationContext, cUser);

    *ppUser = cUser;

    return XSAPI_RESULT_OK;
}
CATCH_RETURN()

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserCreate(
    _Out_ XSAPI_XBOX_LIVE_USER* *ppUser
    ) XSAPI_NOEXCEPT
try
{
    return XboxLiveUserCreateFromSystemUser(nullptr, ppUser);
}
CATCH_RETURN()

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserDelete(
    _In_ XSAPI_XBOX_LIVE_USER* pUser
    ) XSAPI_NOEXCEPT
try
{
    auto singleton = get_xsapi_singleton();
    std::lock_guard<std::mutex> lock(singleton->m_usersLock);
    
    singleton->m_signedInUsers.erase(pUser->xboxUserId);
    delete pUser->pImpl;
    delete pUser;
}
CATCH_RETURN_WITH(;)

HC_RESULT XboxLiveUserSignInExecute(
    _In_opt_ void* context,
    _In_ HC_TASK_HANDLE taskHandle
    )
{
    xbox_live_result<sign_in_result> result;
    auto args = reinterpret_cast<sign_in_taskargs*>(context);
        
    if (args->coreDispatcher == nullptr)
    {
        if (args->signInSilently)
        {
            result = args->pUser->pImpl->cppUser()->signin_silently().get();
        }
        else
        {
            result = args->pUser->pImpl->cppUser()->signin().get();
        }
    }
    else
    {
        if (args->signInSilently)
        {
            result = args->pUser->pImpl->cppUser()->signin_silently(args->coreDispatcher).get();
        }
        else
        {
            result = args->pUser->pImpl->cppUser()->signin(args->coreDispatcher).get();
        }
    }

    args->copy_xbox_live_result(result);

    if (!result.err())
    {
        args->completionRoutinePayload.status = static_cast<XSAPI_SIGN_IN_STATUS>(result.payload().status());
        args->pUser->pImpl->Refresh();
        {
            auto singleton = get_xsapi_singleton();
            std::lock_guard<std::mutex> lock(singleton->m_usersLock);
            singleton->m_signedInUsers[args->pUser->xboxUserId] = args->pUser;
        }
    }

    return HCTaskSetCompleted(taskHandle);
}

XSAPI_RESULT XboxLiveUserSignInHelper(
    _Inout_ XSAPI_XBOX_LIVE_USER* pUser,
    _In_ Platform::Object^ coreDispatcher,
    _In_ bool signInSilently,
    _In_ XSAPI_SIGN_IN_COMPLETION_ROUTINE completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    )
{
    verify_global_init();

    auto args = new sign_in_taskargs(pUser, coreDispatcher, signInSilently);

    return utils::xsapi_result_from_hc_result(
        HCTaskCreate(
            HC_SUBSYSTEM_ID::HC_SUBSYSTEM_ID_XSAPI,
            taskGroupId,
            XboxLiveUserSignInExecute,
            static_cast<void*>(args),
            utils::execute_completion_routine_with_payload<sign_in_taskargs, XSAPI_SIGN_IN_COMPLETION_ROUTINE>,
            static_cast<void*>(args),
            static_cast<void*>(completionRoutine),
            completionRoutineContext,
            nullptr
        ));
}

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserSignIn(
    _Inout_ XSAPI_XBOX_LIVE_USER* pUser,
    _In_ XSAPI_SIGN_IN_COMPLETION_ROUTINE completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    ) XSAPI_NOEXCEPT
try
{
    return XboxLiveUserSignInHelper(pUser, nullptr, false, completionRoutine, completionRoutineContext, taskGroupId);
}
CATCH_RETURN()

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserSignInSilently(
    _Inout_ XSAPI_XBOX_LIVE_USER* pUser,
    _In_ XSAPI_SIGN_IN_COMPLETION_ROUTINE completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    ) XSAPI_NOEXCEPT
try
{
    return XboxLiveUserSignInHelper(pUser, nullptr, true, completionRoutine, completionRoutineContext, taskGroupId);
}
CATCH_RETURN()

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserSignInWithCoreDispatcher(
    _Inout_ XSAPI_XBOX_LIVE_USER* pUser,
    _In_ Platform::Object^ coreDispatcher,
    _In_ XSAPI_SIGN_IN_COMPLETION_ROUTINE completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    ) XSAPI_NOEXCEPT
try
{
    return XboxLiveUserSignInHelper(pUser, coreDispatcher, false, completionRoutine, completionRoutineContext, taskGroupId);
}
CATCH_RETURN()

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserSignInSilentlyWithCoreDispatcher(
    _Inout_ XSAPI_XBOX_LIVE_USER* pUser,
    _In_ Platform::Object^ coreDispatcher,
    _In_ XSAPI_SIGN_IN_COMPLETION_ROUTINE completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    ) XSAPI_NOEXCEPT
try
{
    return XboxLiveUserSignInHelper(pUser, coreDispatcher, true, completionRoutine, completionRoutineContext, taskGroupId);
}
CATCH_RETURN()

HC_RESULT XboxLiveUserGetTokenAndSignatureExecute(
    _In_opt_ void* context,
    _In_ HC_TASK_HANDLE taskHandle
    )
{
    auto args = reinterpret_cast<get_token_and_signature_taskargs*>(context);

    auto result = args->pUser->pImpl->cppUser()->get_token_and_signature(
        args->httpMethod,
        args->url,
        args->headers,
        args->requestBodyString)
        .get();

    args->copy_xbox_live_result(result);

    if (!result.err())
    {
        auto cppPayload = result.payload();
        XSAPI_TOKEN_AND_SIGNATURE_RESULT& payload = args->completionRoutinePayload;

        args->token = utils::to_utf8string(cppPayload.token());
        payload.token = args->token.data();

        args->signature = utils::to_utf8string(cppPayload.signature());
        payload.signature = args->signature.data();

        args->xboxUserId = utils::to_utf8string(cppPayload.xbox_user_id());
        payload.xboxUserId = args->xboxUserId.data();

        args->gamertag = utils::to_utf8string(cppPayload.gamertag());
        payload.gamertag = args->gamertag.data();

        args->xboxUserHash = utils::to_utf8string(cppPayload.xbox_user_hash());
        payload.xboxUserHash = args->xboxUserHash.data();

        args->ageGroup = utils::to_utf8string(cppPayload.age_group());
        payload.ageGroup = args->ageGroup.data();

        args->privileges = utils::to_utf8string(cppPayload.privileges());
        payload.privileges = args->privileges.data();

        args->webAccountId = utils::to_utf8string(cppPayload.web_account_id());
        payload.webAccountId = args->webAccountId.data();
    }

    return HCTaskSetCompleted(taskHandle);
}

XSAPI_DLLEXPORT XSAPI_RESULT XBL_CALLING_CONV
XboxLiveUserGetTokenAndSignature(
    _Inout_ XSAPI_XBOX_LIVE_USER* pUser,
    _In_ PCSTR httpMethod,
    _In_ PCSTR url,
    _In_ PCSTR headers,
    _In_ PCSTR requestBodyString,
    _In_ XSAPI_GET_TOKEN_AND_SIGNATURE_COMPLETION_ROUTINE completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    ) XSAPI_NOEXCEPT
try
{
    verify_global_init();

    auto args = new get_token_and_signature_taskargs(
        pUser,
        httpMethod,
        url,
        headers,
        requestBodyString);

    return utils::xsapi_result_from_hc_result(
        HCTaskCreate(
            HC_SUBSYSTEM_ID::HC_SUBSYSTEM_ID_XSAPI,
            taskGroupId,
            XboxLiveUserGetTokenAndSignatureExecute,
            static_cast<void*>(args),
            utils::execute_completion_routine_with_payload<get_token_and_signature_taskargs, XSAPI_GET_TOKEN_AND_SIGNATURE_COMPLETION_ROUTINE>,
            static_cast<void*>(args),
            static_cast<void*>(completionRoutine),
            completionRoutineContext,
            nullptr
        ));
}
CATCH_RETURN()

XSAPI_DLLEXPORT FUNCTION_CONTEXT XBL_CALLING_CONV
AddSignOutCompletedHandler(
    _In_ XSAPI_SIGN_OUT_COMPLETED_HANDLER signOutHandler
    ) XSAPI_NOEXCEPT
try
{
    verify_global_init();

    return xbox_live_user::add_sign_out_completed_handler(
        [signOutHandler](const xbox::services::system::sign_out_completed_event_args& args)
        {
            auto singleton = get_xsapi_singleton();
            std::lock_guard<std::mutex> lock(singleton->m_usersLock);

            auto iter = singleton->m_signedInUsers.find(utils::to_utf8string(args.user()->xbox_user_id()));
            if (iter != singleton->m_signedInUsers.end())
            {
                iter->second->pImpl->Refresh();
                signOutHandler(iter->second);
            }
        });
}
CATCH_RETURN_WITH(-1)

XSAPI_DLLEXPORT void XBL_CALLING_CONV
RemoveSignOutCompletedHandler(
    _In_ FUNCTION_CONTEXT context
    ) XSAPI_NOEXCEPT
try
{
    verify_global_init();
    xbox_live_user::remove_sign_out_completed_handler(context);
}
CATCH_RETURN_WITH(;)