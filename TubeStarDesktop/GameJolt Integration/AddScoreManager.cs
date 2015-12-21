using Newtonsoft.Json;
using System;

namespace TubeStar
{
    public enum HighScoreTable
    {
        Default = 11980,
        ChallengeMode = 14655,
    }

    public class AddScoreManager : BaseManager
    {
        public static Uri GetAddScoreUri(string userName, double score)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/scores/add/?format=json&game_id=11858&score={0}&sort={1}&guest={2}&table_id={3}",
                HttpHelpers.UrlEncoding(score.ToCurrencyString()),
                HttpHelpers.UrlEncoding(score.ToString()),
                HttpHelpers.UrlEncoding(userName),
                Player.Current.ChallengeMode ? (int)HighScoreTable.ChallengeMode : (int)HighScoreTable.Default);
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }

        public static Uri GetGameJoltAddScoreUri(double score)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/scores/add/?format=json&game_id=11858&score={0}&sort={1}&username={2}&user_token={3}&table_id={4}",
                HttpHelpers.UrlEncoding(score.ToCurrencyString()),
                HttpHelpers.UrlEncoding(score.ToString()),
                HttpHelpers.UrlEncoding(Settings.GameJoltLogin),
                HttpHelpers.UrlEncoding(Settings.GameJoltToken),
                Player.Current.ChallengeMode ? (int)HighScoreTable.ChallengeMode : (int)HighScoreTable.Default);
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }
    }

    [JsonObject()]
    public class AddScoreResult
    {
        [JsonProperty("response")]
        public AddScoreResponse Response { get; set; }
    }

    [JsonObject()]
    public class AddScoreResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}