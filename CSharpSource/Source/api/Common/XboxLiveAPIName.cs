// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// 
#if XDK_API
namespace Plugin.Microsoft.Xbox.Services
#else
namespace Microsoft.Xbox.Services
#endif
{
    public enum XboxLiveAPIName : uint
    {
        Unspecified = 0,
        AllocateCluster,
        AllocateClusterInline,
        AllocateSessionHost,
        BrowseCatalogBundlesHelper,
        BrowseCatalogHelper,
        CheckMultiplePermissionsWithMultipleTargetUsers,
        CheckPermissionWithTargetUser,
        ClearActivity,
        ClearSearchHandle,
        ConsumeInventoryItem,
        CreateMatchTicket,
        DeleteBlob,
        DeleteMatchTicket,
        DownloadBlob,
        GetAchievement,
        GetAchievements,
        GetActivitiesForSocialGroup,
        GetActivitiesForUsers,
        GetAvoidOrMuteList,
        GetBlobMetadata,
        GetBroadcasts,
        GetCatalogItemDetails,
        GetConfiguration,
        GetCurrentSession,
        GetCurrentSessionByHandle,
        GetGameClips,
        GetGameServerMetadata,
        GetHopperStatistics,
        GetInventoryItem,
        GetInventoryItems,
        GetLeaderboardForSocialGroupInternal,
        GetLeaderboardInternal,
        GetMatchTicketDetails,
        GetMultipleUserStatisticsForMultipleServiceConfigurations,
        GetPresence,
        GetPresenceForMultipleUsers,
        GetPresenceForSocialGroup,
        GetQualityOfServiceServers,
        GetQuota,
        GetQuotaForSessionStorage,
        GetSearchHandles,
        GetSessionHostAllocationStatus,
        GetSessions,
        GetSingleUserStatistics,
        GetSocialGraph,
        GetSocialRelationships,
        GetStatsValueDocument,
        GetTicketStatus,
        GetTournaments,
        GetTournamentDetails,
        GetTeams,
        GetTeamDetails,
        GetUserProfiles,
        GetProfileInfo,
        GetUserProfilesForSocialGroup,
        RegisterTeam,
        SendInvites,
        SetActivity,
        SetPresenceHelper,
        SetSearchHandle,
        SetTransferHandle,
        SubmitBatchReputationFeedback,
        SubmitReputationFeedback,
        SubscribeToNotifications,
        UpdateAchievement,
        UpdateStatsValueDocument,
        UploadBlob,
        VerifyStrings,
        WriteSessionUsingSubpath,
        XboxOnePinsAddItem,
        XboxOnePinsContainsItem,
        XboxOnePinsRemoveItem
    };
}

