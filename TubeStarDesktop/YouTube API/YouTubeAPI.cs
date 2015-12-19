using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace TubeStar
{
    public class YouTubeAPI
    {
        private const string ApiKey = "AIzaSyDL-vhmr5xpdU0d4sMDUIzQskZnC26ci24";

        public static Uri GetRandomImages(string search, int max)
        {
            search = search.Replace(' ', ',');
            var uriString = String.Format("https://www.googleapis.com/youtube/v3/search?videoSyndicated=true&part=snippet&q={0}&type=video&&maxResults={1}&fields=nextPageToken,prevPageToken,items(id(videoId))&key={2}{3}",
                HttpHelpers.UrlEncoding(search), max, ApiKey, Settings.UseCreativeCommons ? "&videoLicense=creativeCommon" : "");
            return new Uri(uriString);
        }

        public static Uri GetRandomImagesWithTitle(string search, int max)
        {
            search = search.Replace(' ', ',');
            var uriString = String.Format("https://www.googleapis.com/youtube/v3/search?videoSyndicated=true&part=snippet&q={0}&type=video&&maxResults={1}&fields=nextPageToken,prevPageToken,items(id(videoId),snippet(title))&key={2}{3}",
                HttpHelpers.UrlEncoding(search), max, ApiKey, Settings.UseCreativeCommons ? "&videoLicense=creativeCommon" : "");
            return new Uri(uriString);
        }

        public static Uri GetRandomComments(string videoId, int max)
        {
            var uriString = String.Format("https://www.googleapis.com/youtube/v3/commentThreads?part=snippet&videoId={0}&maxResults={1}&fields=items/snippet/topLevelComment/snippet/textDisplay&key={2}",
                videoId, max, ApiKey);
            return new Uri(uriString);
        }

        public static Uri GetPhotoUri(string id)
        {
            return new Uri(String.Format("http://i.ytimg.com/vi/{0}/hqdefault.jpg", id));
        }

        public static Uri GetSmallPhotoUri(string id)
        {
            return new Uri(String.Format("http://i.ytimg.com/vi/{0}/default.jpg", id));
        }

        public static Uri GetLinkUri(string id)
        {
            return new Uri(String.Format("http://www.youtube.com/watch?v={0}", id));
        }
    }

    [JsonObject()]
    public class YouTubeCommentResponse
    {
        [JsonProperty("items")]
        public List<YouTubeCommentItemSnippet> Entries { get; set; }
    }

    [JsonObject()]
    public class YouTubeCommentItemSnippet
    {
        [JsonProperty("snippet")]
        public YouTubeTopLevelComment Snippet { get; set; }
    }

    [JsonObject()]
    public class YouTubeTopLevelComment
    {
        [JsonProperty("topLevelComment")]
        public YouTubeCommentSnippet TopLevelComment { get; set; }
    }

    [JsonObject()]
    public class YouTubeCommentSnippet
    {
        [JsonProperty("snippet")]
        public YouTubeComment Snippet { get; set; }
    }

    [JsonObject()]
    public class YouTubeComment
    {
        [JsonProperty("textDisplay")]
        public string Comment { get; set; }
    }

    [JsonObject()]
    public class YouTubeSearchResponse
    {
        [JsonProperty("prevPageToken")]
        public string PreviousPageToken { get; set; }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }

        [JsonProperty("items")]
        public List<YouTubeSearchEntry> Entries { get; set; }
    }

    [JsonObject()]
    public class YouTubeSearchEntry
    {
        [JsonProperty("id")]
        public YouTubeSearchId Id { get; set; }

        [JsonProperty("snippet")]
        public YouTubeSearchTitle Snippet { get; set; }
    }

    [JsonObject()]
    public class YouTubeSearchTitle
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    [JsonObject()]
    public class YouTubeSearchId
    {
        [JsonProperty("videoId")]
        public string VideoId { get; set; }
    }
}