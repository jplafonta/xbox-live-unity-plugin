// Copyright (c) Microsoft Corporation
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#if XDK_API
namespace Plugin.Microsoft.Xbox.Services.Leaderboard
#else
namespace Microsoft.Xbox.Services.Leaderboard
#endif
{
    using Newtonsoft.Json;

    public class LeaderboardColumn
    {
        [JsonProperty("type")]
        public LeaderboardStatType StatisticType { get; set; }

        [JsonProperty("statName")]
        public string StatisticName { get; set; }
    }
}