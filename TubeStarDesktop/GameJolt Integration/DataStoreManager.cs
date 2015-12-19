using System;
using Newtonsoft.Json;

namespace TubeStar
{
    public class DataStoreManager : BaseManager
    {
        public static Uri GetSetDataUri(string key)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/data-store/set/?game_id=11858&key={0}",
                HttpHelpers.UrlEncoding(key));
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }

        public static Uri GetSetDataUri(string key, string data)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/data-store/set/?game_id=11858&key={0}&data={1}",
                HttpHelpers.UrlEncoding(key),
                HttpHelpers.UrlEncoding(data));
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }

        public static Uri GetDataUri(string key)
        {
            var tableUri = String.Format("http://gamejolt.com/api/game/v1/data-store/?format=json&game_id=11858&key={0}",
                HttpHelpers.UrlEncoding(key));
            return new Uri(String.Format("{0}&signature={1}", tableUri, GetSigniture(tableUri)));
        }
    }

    [JsonObject()]
    public class SetDataResult
    {
        [JsonProperty("response")]
        public SetDataResponse Response { get; set; }
    }

    [JsonObject()]
    public class SetDataResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }

    [JsonObject()]
    public class GetDataResult
    {
        [JsonProperty("response")]
        public GetDataResponse Response { get; set; }
    }

    [JsonObject()]
    public class GetDataResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }
    }
}