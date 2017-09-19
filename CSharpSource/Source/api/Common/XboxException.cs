// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    using global::System;

    public class XboxException : Exception
    {
        public XboxException()
        {
        }

        public XboxException(string message) : base(message)
        {
        }

        public XboxException(int HResult, string message) : base(message)
        {
            this.HResult = HResult;
        }

        public XboxException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}