using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TubeStar
{
    public class FetchScoreManager : BaseManager
    {
        public static Uri GetFetchScoresUri(HighScoreTable table)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/scores/?format=json&game_id=11858&table_id={0}", (int)table);
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }
    }

    [JsonObject()]
    public class FetchScoresResult
    {
        [JsonProperty("response")]
        public FetchScoresResponse Response { get; set; }
    }

    [JsonObject()]
    public class FetchScoresResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("scores")]
        public List<ScoreItem> Scores { get; set; }

        public FetchScoresResponse()
        {
            Scores = new List<ScoreItem>();
        }
    }

    [JsonObject()]
    public class ScoreItem
    {
        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("sort")]
        public double Sort { get; set; }

        [JsonProperty("guest")]
        public string GuestName { get; set; }

        [JsonProperty("stored")]
        public string Stored { get; set; }

        [JsonProperty("user")]
        public string UserName { get; set; }
    }
}