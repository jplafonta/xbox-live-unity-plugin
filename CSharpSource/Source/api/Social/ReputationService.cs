// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Social
#else
namespace Microsoft.Xbox.Services.Social
#endif
{
    public class ReputationService
    {

        public Task SubmitReputationFeedbackAsync(string xboxUserId, ReputationFeedbackType reputationFeedbackType, string sessionName, string reasonMessage, string evidenceResourceId)
        {
            throw new NotImplementedException();
        }

        public Task SubmitBatchReputationFeedbackAsync(Microsoft.Xbox.Services.Social.ReputationFeedbackItem[] feedbackItems)
        {
            throw new NotImplementedException();
        }

    }
}
