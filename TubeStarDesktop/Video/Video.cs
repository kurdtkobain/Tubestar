using System;
using System.Collections.Generic;

namespace TubeStar
{
    public class Video : UniqueObject
    {
        public string Name { get; set; }
        public VideoCategory Category { get; set; }

        public int? ShootQuality { get; set; }
        public int? EditQuality { get; set; }
        public int? Quality { get; set; }
        public int? QualityBias { get; set; }
        public bool ExternalQuality { get; set; }

        public int Views { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public List<VideoComment> Comments { get; set; }

        public bool HasBeenEdited { get; set; }
        public bool HasBeenRendered { get; set; }
        public bool HasBeenReleased { get; set; }

        public int ExtraShootingHours { get; set; }
        public int ExtraEditingHours { get; set; }
        public int ExtraRenderHours { get; set; }
        public int Cost { get; set; }

        public int Iterations { get; set; }

        public double CostPerView { get; set; }
        public double OnceOffCost { get; set; }

        public List<VideoAttribute> Attributes { get; set; }

        public string YouTubeVideoId { get; set; }
        public byte[] ImageBytes { get; set; }

        public event Action OnImageFetched;

        public bool IsSuspended { get; set; }

        public Video()
            : base()
        {
            Comments = new List<VideoComment>();
            Attributes = new List<VideoAttribute>();
        }

        public int GenerateQuality()
        {
            if (ExternalQuality && Quality.HasValue)
                return Quality.Value;

            var quality = 0;

            if (!ShootQuality.HasValue)
            {
                ShootQuality = 0;

                int schoolSkill = 0;
                Studies.Current.All.ForEach(s =>
                {
                    if (s.IsCompleted && s.SkillModifierType == SkillModifierType.Shooting)
                        schoolSkill += s.SkillModifier;
                });

                int itemSkill = 0;
                StoreItems.Current.All.ForEach(s =>
                {
                    if (s.Purchased && s.SkillModifierType == SkillModifierType.Shooting)
                        itemSkill += s.SkillModifier;
                });

                ShootQuality += RandomHelpers.RandomInt(Player.Current.ShootingSkill - schoolSkill);
                ShootQuality += RandomHelpers.RandomInt(schoolSkill);
                ShootQuality += RandomHelpers.RandomInt(itemSkill);
                ShootQuality += RandomHelpers.RandomInt(ExtraShootingHours * 2);
                ShootQuality += RandomHelpers.RandomInt((Cost / 100) * 5);

                if (Attributes.Contains(VideoAttributes.LearnFromMistakes))
                {
                    var remove = RandomHelpers.RandomInt(5);
                    ShootQuality -= 5 + remove;
                    Player.Current.ShootingSkill += remove;
                }

                if (StoreItems.Current.Consultant.Purchased)
                {
                    ShootQuality += StoreItems.Current.Consultant.SkillModifier;
                }
            }

            quality += ShootQuality.Value;

            if (HasBeenEdited)
            {
                if (!EditQuality.HasValue)
                {
                    EditQuality = 0;

                    int schoolSkill = 0;
                    Studies.Current.All.ForEach(s =>
                    {
                        if (s.IsCompleted && s.SkillModifierType == SkillModifierType.PostProduction)
                            schoolSkill += s.SkillModifier;
                    });

                    int itemSkill = 0;
                    StoreItems.Current.All.ForEach(s =>
                    {
                        if (s.Purchased && s.SkillModifierType == SkillModifierType.PostProduction)
                            itemSkill += s.SkillModifier;
                    });
                    EditQuality += RandomHelpers.RandomInt(Player.Current.PostProductionSkill - schoolSkill);
                    EditQuality += RandomHelpers.RandomInt(schoolSkill);
                    EditQuality += RandomHelpers.RandomInt(itemSkill);
                    EditQuality += RandomHelpers.RandomInt(ExtraEditingHours * 3);

                    if (Attributes.Contains(VideoAttributes.LearnFromMistakes))
                    {
                        var remove = RandomHelpers.RandomInt(5);
                        EditQuality -= 5 + remove;
                        Player.Current.PostProductionSkill += remove;
                    }
                }

                quality += EditQuality.Value;
            }

            if (Attributes.Contains(VideoAttributes.Cats))
            {
                quality += VideoAttributes.Cats.QualityIncrease;
            }

            quality = Math.Max(quality, 0);
            quality = Math.Min(quality, 100);

            //Imperfection bias
            if (quality == 100)
            {
                if (!QualityBias.HasValue)
                {
                    QualityBias = RandomHelpers.RandomInt(5);
                }
                quality -= QualityBias.Value;
            }

            Quality = quality;
            return Quality.Value;
        }

        public int GetCommentsCount()
        {
            int count = Comments.Count;
            foreach (var comment in Comments)
            {
                count += comment.Likes;
            }
            return count;
        }

        public int GenerateRivalQuality(int shootingSkill, int postProductionSkill)
        {
            var quality = RandomHelpers.RandomInt(shootingSkill) + RandomHelpers.RandomInt(postProductionSkill);

            quality = Math.Max(quality, 0);
            quality = Math.Min(quality, 100);

            //Imperfection bias
            if (quality == 100)
            {
                if (!QualityBias.HasValue)
                {
                    QualityBias = RandomHelpers.RandomInt(5);
                }
                quality -= QualityBias.Value;
            }

            ExternalQuality = true;
            Quality = quality;
            return Quality.Value;
        }

        public void ExternalSetImageBytes(byte[] bytes)
        {
            ImageBytes = bytes;
            if (OnImageFetched != null)
                OnImageFetched();
        }

        public void FetchRandomImage(bool useCategory = true)
        {
            int randomImageCount = 50;
            var searchString = useCategory ? String.Format("{0},{1}", Category.GetString(), Name) : Name;
            WebClientHelpers.Download<YouTubeSearchResponse>(YouTubeAPI.GetRandomImages(searchString, randomImageCount), (response) =>
            {
                if (response != null && response.Entries != null && response.Entries.Count > 0)
                {
                    DoFetchImage(randomImageCount, response);
                }
                else
                {
                    //Limit to category
                    WebClientHelpers.Download<YouTubeSearchResponse>(YouTubeAPI.GetRandomImages(Category.GetString(), randomImageCount), (r) =>
                    {
                        if (r != null && r.Entries != null && r.Entries.Count > 0)
                        {
                            DoFetchImage(randomImageCount, r);
                        }
                    }, () =>
                    {
                        SetErrorImage();
                    });
                }
            }, () =>
            {
                SetErrorImage();
            });
        }

        private void DoFetchImage(int randomImageCount, YouTubeSearchResponse response)
        {
            var randomInt = RandomHelpers.RandomInt(Math.Min(randomImageCount, response.Entries.Count));
            var youTubeVideoId = response.Entries[randomInt].Id.VideoId;
            SetImageFromId(youTubeVideoId);
        }

        public void SetImageFromId(string youTubeVideoId)
        {
            Uri photoUri = YouTubeAPI.GetPhotoUri(youTubeVideoId);
            Uri linkUri = YouTubeAPI.GetLinkUri(youTubeVideoId);

            WebClientHelpers.DownloadImage(photoUri, (result) =>
            {
                YouTubeVideoId = youTubeVideoId;
                ImageBytes = StreamHelpers.StreamToBytes(result);

                if (OnImageFetched != null)
                    OnImageFetched();
            }, () =>
            {
                SetErrorImage();
            });
        }

        public void SetErrorImage()
        {
            var info = System.Windows.Application.GetResourceStream(new Uri("../Resources/InternetDown.jpg", UriKind.Relative));
            ExternalSetImageBytes(StreamHelpers.StreamToBytes(info.Stream));
        }
    }
}