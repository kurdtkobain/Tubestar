using System.Collections.Generic;
using System.Xml.Serialization;

namespace TubeStar
{
    [XmlInclude(typeof(Rival1))]
    [XmlInclude(typeof(Rival2))]
    [XmlInclude(typeof(Rival3))]
    [XmlInclude(typeof(Rival4))]
    [XmlInclude(typeof(Rival5))]
    [XmlInclude(typeof(Rival6))]
    [XmlInclude(typeof(Rival7))]
    [XmlInclude(typeof(Rival8))]
    [XmlInclude(typeof(Rival9))]
    [XmlInclude(typeof(Rival10))]
    [XmlInclude(typeof(Rival11))]
    [XmlInclude(typeof(Rival12))]
    [XmlInclude(typeof(Rival13))]
    [XmlInclude(typeof(Rival14))]
    [XmlInclude(typeof(Rival15))]
    public class Rival
    {
        private Dictionary<string, List<YouTubeSearchEntry>> _cacheVideos;

        public string Name { get; set; }

        public int ShootingSkill { get; set; }
        public int PostProductionSkill { get; set; }
        public int InitialSubscribers { get; set; }

        public string VideoKeyword1 { get; set; }
        public string VideoKeyword2 { get; set; }
        public string VideoKeyword3 { get; set; }
        public string VideoKeyword4 { get; set; }

        public Channel Channel { get; set; }
        public List<string> UsedVideos { get; set; }

        public Rival()
        {
            _cacheVideos = new Dictionary<string, List<YouTubeSearchEntry>>();
            UsedVideos = new List<string>();
        }

        public void CreateChannel()
        {
            Channel = new Channel();
            Channel.Name = Name;
            Channel.IsRivalChannel = true;
            Channel.Advertising = AdvertisingStrategy.High;
            Channel.Subscribers = InitialSubscribers;

            AddNewVideo();
        }

        public void AddNewVideo()
        {
            string word;
            var rand = RandomHelpers.RandomInt(100);
            if (rand < 25)
                word = VideoKeyword1;
            else if (rand < 50)
                word = VideoKeyword2;
            else if (rand < 75)
                word = VideoKeyword2;
            else
                word = VideoKeyword4;

            if (_cacheVideos.ContainsKey(word))
            {
                DoAddNewVideo(_cacheVideos[word]);
            }
            else
            {
                WebClientHelpers.Download<YouTubeSearchResponse>(YouTubeAPI.GetRandomImagesWithTitle(word, 50), (r) =>
                {
                    if (r != null && r.Entries != null && r.Entries.Count > 0)
                    {
                        _cacheVideos[word] = r.Entries;
                        DoAddNewVideo(r.Entries);
                    }
                }, null);
            }
        }

        private void DoAddNewVideo(List<YouTubeSearchEntry> entries)
        {
            try
            {
                var randCount = RandomHelpers.RandomInt(entries.Count);

                int attempt = 0;
                bool found = false;
                while (true)
                {
                    var videoId = entries[randCount].Id.VideoId;
                    if (!UsedVideos.Contains(videoId))
                    {
                        found = true;
                        break;
                    }

                    randCount = RandomHelpers.RandomInt(entries.Count);

                    attempt++;
                    if (attempt == 5)
                        break;
                }

                if (found)
                {
                    UsedVideos.Add(entries[randCount].Id.VideoId);

                    Video video = new Video();
                    video.Name = entries[randCount].Snippet.Title;
                    video.Category = VideoCategory.Cats;
                    video.YouTubeVideoId = entries[randCount].Id.VideoId;
                    video.GenerateRivalQuality(ShootingSkill, PostProductionSkill);

                    var photoUri = YouTubeAPI.GetPhotoUri(entries[randCount].Id.VideoId);
                    WebClientHelpers.DownloadImage(photoUri, (stream) =>
                    {
                        if (stream != null)
                        {
                            video.ExternalSetImageBytes(StreamHelpers.StreamToBytes(stream));
                        }
                    }, () =>
                    {
                        video.SetErrorImage();
                    });

                    Channel.Videos.Add(video);
                }
            }
            catch { }
        }
    }
}