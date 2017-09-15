// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma once
#include "types_c.h"
#include "xsapi/errors_c.h"

#if defined(__cplusplus)
extern "C" {
#endif

struct XboxLiveUserImpl;

typedef struct XboxLiveUser
{
    PCSTR_T xboxUserId;
    PCSTR_T gamertag;
    PCSTR_T ageGroup;
    PCSTR_T privileges;
    bool isSignedIn;

#if XDK_API
    Windows::Xbox::System::User^ xboxSystemUser;
#endif
#if UWP_API
    PCSTR_T webAccountId;
    Windows::System::User^ windowsSystemUser;
#endif
    
    XboxLiveUserImpl *pImpl;
} XboxLiveUser;

#if !XDK_API
typedef enum SIGN_IN_STATUS
{
    /// <summary>
    /// Signed in successfully.
    /// </summary>
    SUCCESS = 0,

    /// <summary>
    /// Need to invoke the signin API (w/ UX) to let the user take necessary actions for the sign-in operation to continue.
    /// Can only be returned from signin_silently().
    /// </summary>
    USER_INTERACTION_REQUIRED,

    /// <summary>
    /// The user decided to cancel the sign-in operation.
    /// Can only be returned from signin().
    /// </summary>
    USER_CANCEL
} SIGN_IN_STATUS;

typedef struct SignInResultPayload
{
    SIGN_IN_STATUS status;
} SignInResultPayload;

typedef struct SignInResult
{
    XboxLiveResult result;
    SignInResultPayload payload;
} SignInResult;

typedef struct TokenAndSignatureResultPayload
{
    PCSTR_T token;
    PCSTR_T signature;
    PCSTR_T xboxUserId;
    PCSTR_T gamertag;
    PCSTR_T xboxUserHash;
    PCSTR_T ageGroup;
    PCSTR_T privileges;
    PCSTR_T webAccountId;

} TokenAndSignatureResultPayload;

typedef struct TokenAndSignatureResult
{
    XboxLiveResult result;
    TokenAndSignatureResultPayload payload;
} TokenAndSignatureResult;

#endif // !XDK_API

#if XDK_API

XSAPI_DLLEXPORT XboxLiveUser* XBL_CALLING_CONV
XboxLiveUserCreate(
    _In_ Windows::Xbox::System::User^ xboxSystemUser
    );

#else 
XSAPI_DLLEXPORT XboxLiveUser* XBL_CALLING_CONV
XboxLiveUserCreate();

#if UWP_API
XSAPI_DLLEXPORT XboxLiveUser* XBL_CALLING_CONV
XboxLiveUserCreateFromSystemUser(
    _In_ Windows::System::User^ creationContext
    );

#endif // UWP_API
#endif // XDK_API

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserDelete(
    _In_ XboxLiveUser *user
    );

#if !XDK_API
typedef void(*SignInCompletionRoutine)(
    _In_ SignInResult result,
    _In_opt_ void* context
    );

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignIn(
    _Inout_ XboxLiveUser* user,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    );

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignInSilently(
    _Inout_ XboxLiveUser* user,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    );

#if UWP_API
XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignInWithCoreDispatcher(
    _Inout_ XboxLiveUser* user,
    _In_ Platform::Object^ coreDispatcher,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    );

XSAPI_DLLEXPORT void XBL_CALLING_CONV
XboxLiveUserSignInSilentlyWithCoreDispatcher(
    _Inout_ XboxLiveUser* user,
    _In_ Platform::Object^ coreDispatcher,
    _In_ SignInCompletionRoutine completionRoutine,
    _In_opt_ void* completionRoutineContext,
    _In_ uint64_t taskGroupId
    );
#endif // UWP_API

typedef void(*GetTokenAndSignatureCompletionRoutine)(
    _In_ TokenAndSignatureResult result,
    _In_opt_ void* context
    );

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
    );

#else // !XDK_API
typedef void(*SignInCompletedHandler)(
    _In_ XboxLiveUser* user
    );

XSAPI_DLLEXPORT function_context XBL_CALLING_CONV
AddSignInCompletedHandler(
    _In_ SignInCompletedHandler signInHandler
    );

XSAPI_DLLEXPORT void XBL_CALLING_CONV
RemoveSignInCompletedHandler(
    _In_ function_context context
    );

#endif // !XDK_API

typedef void(*SignOutCompletedHandler)(
    _In_ XboxLiveUser* user
    );

XSAPI_DLLEXPORT function_context XBL_CALLING_CONV
AddSignOutCompletedHandler(
    _In_ SignOutCompletedHandler signOutHandler
    );

XSAPI_DLLEXPORT void XBL_CALLING_CONV
RemoveSignOutCompletedHandler(
    _In_ function_context context
    );

#if defined(__cplusplus)
} // end extern "C"
#endif // defined(__cplusplus)