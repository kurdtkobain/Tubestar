using System;
using Newtonsoft.Json;

namespace TubeStar
{
    public class AuthUserManager : BaseManager
    {
        public static Uri GetAuthUserUri(string userName, string token)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/users/auth/?format=json&game_id=11858&username={0}&user_token={1}",
                HttpHelpers.UrlEncoding(userName), HttpHelpers.UrlEncoding(token));
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }
    }

    [JsonObject()]
    public class AuthUserResult
    {
        [JsonProperty("response")]
        public AuthUserResponse Response { get; set; }
    }

    [JsonObject()]
    public class AuthUserResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}