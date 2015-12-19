using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TubeStar
{
    public enum CommentType
    {
        Like,
        Dislike,
        Random,
        Subscribe,
        UnsubscribeCategory,
        UnsubscribeQuality,
    }

    public static class CommentGenerator
    {
        private static Dictionary<string, List<string>> _videoComments;

        static CommentGenerator()
        {
            _videoComments = new Dictionary<string, List<string>>();
        }

        public static void GenerateRealComments(string videoId)
        {
            if (!String.IsNullOrEmpty(videoId) && !_videoComments.ContainsKey(videoId))
            {
                WebClientHelpers.Download<YouTubeCommentResponse>(YouTubeAPI.GetRandomComments(videoId, 50), (r) =>
                {
                    if (r != null && r.Entries != null && r.Entries.Count > 0)
                    {
                        _videoComments[videoId] = r.Entries.Select(f => f.Snippet.TopLevelComment.Snippet.Comment).ToList();
                    }
                }, null);
            }
        }

        public static string GenerateComment(Video video, CommentType type)
        {
            switch (type)
            {
                case (CommentType.Like): return LikeComment(video);
                case (CommentType.Dislike): return DislikeComment(video);
                case (CommentType.Subscribe): return SubscribeComment(video);
                case (CommentType.UnsubscribeCategory): return UnsubscribeCategoryComment(video);
                case (CommentType.UnsubscribeQuality): return UnsubscribeQualityComment(video);
                default: return RandomComment(video);
            }
        }

        private static Lazy<List<string>> LikeComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.LikeComment1.Translate(),
                        CommentStrings.LikeComment2.Translate(),
                        CommentStrings.LikeComment3.Translate(),
                        CommentStrings.LikeComment4.Translate(),
                        CommentStrings.LikeComment5.Translate(),
                        CommentStrings.LikeComment6.Translate(),
                        CommentStrings.LikeComment7.Translate(),
                        CommentStrings.LikeComment8.Translate(),
                        CommentStrings.LikeComment9.Translate(),
                        CommentStrings.LikeComment10.Translate(),
                    };
        });

        private static Lazy<List<string>> DislikeComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.DislikeComment1.Translate(),
                        CommentStrings.DislikeComment2.Translate(),
                        CommentStrings.DislikeComment3.Translate(),
                        CommentStrings.DislikeComment4.Translate(),
                        CommentStrings.DislikeComment5.Translate(),
                        CommentStrings.DislikeComment6.Translate(),
                        CommentStrings.DislikeComment7.Translate(),
                        CommentStrings.DislikeComment8.Translate(),
                        CommentStrings.DislikeComment9.Translate(),
                        CommentStrings.DislikeComment10.Translate(),
                    };
        });

        private static Lazy<List<string>> SubscribeComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.SubscribedComment1.Translate(),
                        CommentStrings.SubscribedComment2.Translate(),
                        CommentStrings.SubscribedComment3.Translate(),
                        CommentStrings.SubscribedComment4.Translate(),
                        CommentStrings.SubscribedComment5.Translate(),
                    };
        });

        private static Lazy<List<string>> AccidentComments = new Lazy<List<string>>(() =>
            {
                return new List<string>()
                    {
                        CommentStrings.FailsComment1.Translate(),
                        CommentStrings.FailsComment2.Translate(),
                        CommentStrings.FailsComment3.Translate(),
                        CommentStrings.FailsComment4.Translate(),
                        CommentStrings.FailsComment5.Translate(),
                        CommentStrings.FailsComment6.Translate(),
                        CommentStrings.FailsComment7.Translate(),
                        CommentStrings.FailsComment8.Translate(),
                        CommentStrings.FailsComment9.Translate(),
                        CommentStrings.FailsComment10.Translate(),
                        CommentStrings.FailsComment11.Translate(),
                        CommentStrings.FailsComment12.Translate(),
                        CommentStrings.FailsComment13.Translate(),
                        CommentStrings.FailsComment14.Translate(),
                        CommentStrings.FailsComment15.Translate(),
                    };
            });

        private static Lazy<List<string>> AnimalVsAnimalComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.AnimalFightingComment1.Translate(),
                        CommentStrings.AnimalFightingComment2.Translate(),
                        CommentStrings.AnimalFightingComment3.Translate(),
                        CommentStrings.AnimalFightingComment4.Translate(),
                        CommentStrings.AnimalFightingComment5.Translate(),
                        CommentStrings.AnimalFightingComment6.Translate(),
                        CommentStrings.AnimalFightingComment7.Translate(),
                        CommentStrings.AnimalFightingComment8.Translate(),
                        CommentStrings.AnimalFightingComment9.Translate(),
                        CommentStrings.AnimalFightingComment10.Translate(),
                        CommentStrings.AnimalFightingComment11.Translate(),
                        CommentStrings.AnimalFightingComment12.Translate(),
                        CommentStrings.AnimalFightingComment13.Translate(),
                        CommentStrings.AnimalFightingComment14.Translate(),
                        CommentStrings.AnimalFightingComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> CatsComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.CatsComment1.Translate(),
                        CommentStrings.CatsComment2.Translate(),
                        CommentStrings.CatsComment3.Translate(),
                        CommentStrings.CatsComment4.Translate(),
                        CommentStrings.CatsComment5.Translate(),
                        CommentStrings.CatsComment6.Translate(),
                        CommentStrings.CatsComment7.Translate(),
                        CommentStrings.CatsComment8.Translate(),
                        CommentStrings.CatsComment9.Translate(),
                        CommentStrings.CatsComment10.Translate(),
                        CommentStrings.CatsComment11.Translate(),
                        CommentStrings.CatsComment12.Translate(),
                        CommentStrings.CatsComment13.Translate(),
                        CommentStrings.CatsComment14.Translate(),
                        CommentStrings.CatsComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> ComedyComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.ComedyComment1.Translate(),
                        CommentStrings.ComedyComment2.Translate(),
                        CommentStrings.ComedyComment3.Translate(),
                        CommentStrings.ComedyComment4.Translate(),
                        CommentStrings.ComedyComment5.Translate(),
                        CommentStrings.ComedyComment6.Translate(),
                        CommentStrings.ComedyComment7.Translate(),
                        CommentStrings.ComedyComment8.Translate(),
                        CommentStrings.ComedyComment9.Translate(),
                        CommentStrings.ComedyComment10.Translate(),
                        CommentStrings.ComedyComment11.Translate(),
                        CommentStrings.ComedyComment12.Translate(),
                        CommentStrings.ComedyComment13.Translate(),
                        CommentStrings.ComedyComment14.Translate(),
                        CommentStrings.ComedyComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> ConspiraryTheoryComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.ConspiraryTheoryComment1.Translate(),
                        CommentStrings.ConspiraryTheoryComment2.Translate(),
                        CommentStrings.ConspiraryTheoryComment3.Translate(),
                        CommentStrings.ConspiraryTheoryComment4.Translate(),
                        CommentStrings.ConspiraryTheoryComment5.Translate(),
                        CommentStrings.ConspiraryTheoryComment6.Translate(),
                        CommentStrings.ConspiraryTheoryComment7.Translate(),
                        CommentStrings.ConspiraryTheoryComment8.Translate(),
                        CommentStrings.ConspiraryTheoryComment9.Translate(),
                        CommentStrings.ConspiraryTheoryComment10.Translate(),
                        CommentStrings.ConspiraryTheoryComment11.Translate(),
                        CommentStrings.ConspiraryTheoryComment12.Translate(),
                        CommentStrings.ConspiraryTheoryComment13.Translate(),
                        CommentStrings.ConspiraryTheoryComment14.Translate(),
                        CommentStrings.ConspiraryTheoryComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> GamingComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.GamingComment1.Translate(),
                        CommentStrings.GamingComment2.Translate(),
                        CommentStrings.GamingComment3.Translate(),
                        CommentStrings.GamingComment4.Translate(),
                        CommentStrings.GamingComment5.Translate(),
                        CommentStrings.GamingComment6.Translate(),
                        CommentStrings.GamingComment7.Translate(),
                        CommentStrings.GamingComment8.Translate(),
                        CommentStrings.GamingComment9.Translate(),
                        CommentStrings.GamingComment10.Translate(),
                        CommentStrings.GamingComment11.Translate(),
                        CommentStrings.GamingComment12.Translate(),
                        CommentStrings.GamingComment13.Translate(),
                        CommentStrings.GamingComment14.Translate(),
                        CommentStrings.GamingComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> HaulComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.ShoppingHaulComment1.Translate(),
                        CommentStrings.ShoppingHaulComment2.Translate(),
                        CommentStrings.ShoppingHaulComment3.Translate(),
                        CommentStrings.ShoppingHaulComment4.Translate(),
                        CommentStrings.ShoppingHaulComment5.Translate(),
                        CommentStrings.ShoppingHaulComment6.Translate(),
                        CommentStrings.ShoppingHaulComment7.Translate(),
                        CommentStrings.ShoppingHaulComment8.Translate(),
                        CommentStrings.ShoppingHaulComment9.Translate(),
                        CommentStrings.ShoppingHaulComment10.Translate(),
                        CommentStrings.ShoppingHaulComment11.Translate(),
                        CommentStrings.ShoppingHaulComment12.Translate(),
                        CommentStrings.ShoppingHaulComment13.Translate(),
                        CommentStrings.ShoppingHaulComment14.Translate(),
                        CommentStrings.ShoppingHaulComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> HowToComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.HowToComment1.Translate(),
                        CommentStrings.HowToComment2.Translate(),
                        CommentStrings.HowToComment3.Translate(),
                        CommentStrings.HowToComment4.Translate(),
                        CommentStrings.HowToComment5.Translate(),
                        CommentStrings.HowToComment6.Translate(),
                        CommentStrings.HowToComment7.Translate(),
                        CommentStrings.HowToComment8.Translate(),
                        CommentStrings.HowToComment9.Translate(),
                        CommentStrings.HowToComment10.Translate(),
                        CommentStrings.HowToComment11.Translate(),
                        CommentStrings.HowToComment12.Translate(),
                        CommentStrings.HowToComment13.Translate(),
                        CommentStrings.HowToComment14.Translate(),
                        CommentStrings.HowToComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> MusicVideoComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.MusicVideoComment1.Translate(),
                        CommentStrings.MusicVideoComment2.Translate(),
                        CommentStrings.MusicVideoComment3.Translate(),
                        CommentStrings.MusicVideoComment4.Translate(),
                        CommentStrings.MusicVideoComment5.Translate(),
                        CommentStrings.MusicVideoComment6.Translate(),
                        CommentStrings.MusicVideoComment7.Translate(),
                        CommentStrings.MusicVideoComment8.Translate(),
                        CommentStrings.MusicVideoComment9.Translate(),
                        CommentStrings.MusicVideoComment10.Translate(),
                        CommentStrings.MusicVideoComment11.Translate(),
                        CommentStrings.MusicVideoComment12.Translate(),
                        CommentStrings.MusicVideoComment13.Translate(),
                        CommentStrings.MusicVideoComment14.Translate(),
                        CommentStrings.MusicVideoComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> NonProfitComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.NonProfitComment1.Translate(),
                        CommentStrings.NonProfitComment2.Translate(),
                        CommentStrings.NonProfitComment3.Translate(),
                        CommentStrings.NonProfitComment4.Translate(),
                        CommentStrings.NonProfitComment5.Translate(),
                        CommentStrings.NonProfitComment6.Translate(),
                        CommentStrings.NonProfitComment7.Translate(),
                        CommentStrings.NonProfitComment8.Translate(),
                        CommentStrings.NonProfitComment9.Translate(),
                        CommentStrings.NonProfitComment10.Translate(),
                        CommentStrings.NonProfitComment11.Translate(),
                        CommentStrings.NonProfitComment12.Translate(),
                        CommentStrings.NonProfitComment13.Translate(),
                        CommentStrings.NonProfitComment14.Translate(),
                        CommentStrings.NonProfitComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> SportComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.SportsComment1.Translate(),
                        CommentStrings.SportsComment2.Translate(),
                        CommentStrings.SportsComment3.Translate(),
                        CommentStrings.SportsComment4.Translate(),
                        CommentStrings.SportsComment5.Translate(),
                        CommentStrings.SportsComment6.Translate(),
                        CommentStrings.SportsComment7.Translate(),
                        CommentStrings.SportsComment8.Translate(),
                        CommentStrings.SportsComment9.Translate(),
                        CommentStrings.SportsComment10.Translate(),
                        CommentStrings.SportsComment11.Translate(),
                        CommentStrings.SportsComment12.Translate(),
                        CommentStrings.SportsComment13.Translate(),
                        CommentStrings.SportsComment14.Translate(),
                        CommentStrings.SportsComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> TheWeirdSideComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.TheWeirdSideComment1.Translate(),
                        CommentStrings.TheWeirdSideComment2.Translate(),
                        CommentStrings.TheWeirdSideComment3.Translate(),
                        CommentStrings.TheWeirdSideComment4.Translate(),
                        CommentStrings.TheWeirdSideComment5.Translate(),
                        CommentStrings.TheWeirdSideComment6.Translate(),
                        CommentStrings.TheWeirdSideComment7.Translate(),
                        CommentStrings.TheWeirdSideComment8.Translate(),
                        CommentStrings.TheWeirdSideComment9.Translate(),
                        CommentStrings.TheWeirdSideComment10.Translate(),
                        CommentStrings.TheWeirdSideComment11.Translate(),
                        CommentStrings.TheWeirdSideComment12.Translate(),
                        CommentStrings.TheWeirdSideComment13.Translate(),
                        CommentStrings.TheWeirdSideComment14.Translate(),
                        CommentStrings.TheWeirdSideComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> TechnologyComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.TechnologyComment1.Translate(),
                        CommentStrings.TechnologyComment2.Translate(),
                        CommentStrings.TechnologyComment3.Translate(),
                        CommentStrings.TechnologyComment4.Translate(),
                        CommentStrings.TechnologyComment5.Translate(),
                        CommentStrings.TechnologyComment6.Translate(),
                        CommentStrings.TechnologyComment7.Translate(),
                        CommentStrings.TechnologyComment8.Translate(),
                        CommentStrings.TechnologyComment9.Translate(),
                        CommentStrings.TechnologyComment10.Translate(),
                        CommentStrings.TechnologyComment11.Translate(),
                        CommentStrings.TechnologyComment12.Translate(),
                        CommentStrings.TechnologyComment13.Translate(),
                        CommentStrings.TechnologyComment14.Translate(),
                        CommentStrings.TechnologyComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> UkuleleCoverComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.UkuleleCoverComment1.Translate(),
                        CommentStrings.UkuleleCoverComment2.Translate(),
                        CommentStrings.UkuleleCoverComment3.Translate(),
                        CommentStrings.UkuleleCoverComment4.Translate(),
                        CommentStrings.UkuleleCoverComment5.Translate(),
                        CommentStrings.UkuleleCoverComment6.Translate(),
                        CommentStrings.UkuleleCoverComment7.Translate(),
                        CommentStrings.UkuleleCoverComment8.Translate(),
                        CommentStrings.UkuleleCoverComment9.Translate(),
                        CommentStrings.UkuleleCoverComment10.Translate(),
                        CommentStrings.UkuleleCoverComment11.Translate(),
                        CommentStrings.UkuleleCoverComment12.Translate(),
                        CommentStrings.UkuleleCoverComment13.Translate(),
                        CommentStrings.UkuleleCoverComment14.Translate(),
                        CommentStrings.UkuleleCoverComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> VlogComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.VlogComment1.Translate(),
                        CommentStrings.VlogComment2.Translate(),
                        CommentStrings.VlogComment3.Translate(),
                        CommentStrings.VlogComment4.Translate(),
                        CommentStrings.VlogComment5.Translate(),
                        CommentStrings.VlogComment6.Translate(),
                        CommentStrings.VlogComment7.Translate(),
                        CommentStrings.VlogComment8.Translate(),
                        CommentStrings.VlogComment9.Translate(),
                        CommentStrings.VlogComment10.Translate(),
                        CommentStrings.VlogComment11.Translate(),
                        CommentStrings.VlogComment12.Translate(),
                        CommentStrings.VlogComment13.Translate(),
                        CommentStrings.VlogComment14.Translate(),
                        CommentStrings.VlogComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> AnimeComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.AnimeComment1.Translate(),
                        CommentStrings.AnimeComment2.Translate(),
                        CommentStrings.AnimeComment3.Translate(),
                        CommentStrings.AnimeComment4.Translate(),
                        CommentStrings.AnimeComment5.Translate(),
                        CommentStrings.AnimeComment6.Translate(),
                        CommentStrings.AnimeComment7.Translate(),
                        CommentStrings.AnimeComment8.Translate(),
                        CommentStrings.AnimeComment9.Translate(),
                        CommentStrings.AnimeComment10.Translate(),
                        CommentStrings.AnimeComment11.Translate(),
                        CommentStrings.AnimeComment12.Translate(),
                        CommentStrings.AnimeComment13.Translate(),
                        CommentStrings.AnimeComment14.Translate(),
                        CommentStrings.AnimeComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> MovieComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.MovieComment1.Translate(),
                        CommentStrings.MovieComment2.Translate(),
                        CommentStrings.MovieComment3.Translate(),
                        CommentStrings.MovieComment4.Translate(),
                        CommentStrings.MovieComment5.Translate(),
                        CommentStrings.MovieComment6.Translate(),
                        CommentStrings.MovieComment7.Translate(),
                        CommentStrings.MovieComment8.Translate(),
                        CommentStrings.MovieComment9.Translate(),
                        CommentStrings.MovieComment10.Translate(),
                        CommentStrings.MovieComment11.Translate(),
                        CommentStrings.MovieComment12.Translate(),
                        CommentStrings.MovieComment13.Translate(),
                        CommentStrings.MovieComment14.Translate(),
                        CommentStrings.MovieComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> CreepypastaComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.CreepypastaComment1.Translate(),
                        CommentStrings.CreepypastaComment2.Translate(),
                        CommentStrings.CreepypastaComment3.Translate(),
                        CommentStrings.CreepypastaComment4.Translate(),
                        CommentStrings.CreepypastaComment5.Translate(),
                        CommentStrings.CreepypastaComment6.Translate(),
                        CommentStrings.CreepypastaComment7.Translate(),
                        CommentStrings.CreepypastaComment8.Translate(),
                        CommentStrings.CreepypastaComment9.Translate(),
                        CommentStrings.CreepypastaComment10.Translate(),
                        CommentStrings.CreepypastaComment11.Translate(),
                        CommentStrings.CreepypastaComment12.Translate(),
                        CommentStrings.CreepypastaComment13.Translate(),
                        CommentStrings.CreepypastaComment14.Translate(),
                        CommentStrings.CreepypastaComment15.Translate(),
                    };
        });

        private static Lazy<List<string>> RandomComments = new Lazy<List<string>>(() =>
        {
            return new List<string>()
                    {
                        CommentStrings.RandomComment1.Translate(),
                        CommentStrings.RandomComment2.Translate(),
                        CommentStrings.RandomComment3.Translate(),
                        CommentStrings.RandomComment4.Translate(),
                        CommentStrings.RandomComment5.Translate(),
                        CommentStrings.RandomComment6.Translate(),
                        CommentStrings.RandomComment7.Translate(),
                        CommentStrings.RandomComment8.Translate(),
                        CommentStrings.RandomComment9.Translate(),
                    };
        });

        private static string LikeComment(Video video)
        {
            List<string> comments = new List<string>(LikeComments.Value);

            if (video.Attributes.Contains(VideoAttributes.Cats))
            {
                
                comments.Add(CommentStrings.LikeCatsComment1.Translate());
                comments.Add(CommentStrings.LikeCatsComment2.Translate());
            }

            if (video.Quality >= 75 && video.Attributes.Contains(VideoAttributes.FanBoyBait))
            {
                comments.Add(CommentStrings.FanboyBaitComment1.Translate());
                comments.Add(CommentStrings.FanboyBaitComment2.Translate());
            }

            if (video.Attributes.Contains(VideoAttributes.Hypnotic))
            {
                comments.Add(CommentStrings.HypnoticComment1.Translate());
                comments.Add(CommentStrings.HypnoticComment2.Translate());
            }

            return comments[RandomHelpers.RandomInt(comments.Count)];
        }

        private static string DislikeComment(Video video)
        {
            List<string> comments = new List<string>(DislikeComments.Value);

            if (video.Attributes.Contains(VideoAttributes.SoBad))
            {
                comments.Add(CommentStrings.SoBadComment1.Translate());
                comments.Add(CommentStrings.SoBadComment2.Translate());
                comments.Add(CommentStrings.SoBadComment3.Translate());
                comments.Add(CommentStrings.SoBadComment4.Translate());
            }

            if (!Studies.Current.ProductionI.IsCompleted || !Studies.Current.PostProductionI.IsCompleted)
            {
                comments.Add(CommentStrings.StudyMoreComment.Translate());
            }

            return comments[RandomHelpers.RandomInt(comments.Count)];
        }

        private static string SubscribeComment(Video video)
        {
            List<string> comments = new List<string>(SubscribeComments.Value);

            if (video.Attributes.Contains(VideoAttributes.Hypnotic))
            {
                comments.Add(CommentStrings.SubscriptionHypnoticComment1.Translate());
                comments.Add(CommentStrings.SubscriptionHypnoticComment2.Translate());
            }

            return comments[RandomHelpers.RandomInt(comments.Count)];
        }

        private static string UnsubscribeQualityComment(Video video)
        {
            List<string> comments = new List<string>();

            comments.Add(CommentStrings.UnsubscribeQualityComment1.Translate());
            comments.Add(CommentStrings.UnsubscribeQualityComment2.Translate());
            comments.Add(CommentStrings.UnsubscribeQualityComment3.Translate());
            comments.Add(CommentStrings.UnsubscribeQualityComment4.Translate());
            comments.Add(CommentStrings.UnsubscribeQualityComment5.Translate());

            return comments[RandomHelpers.RandomInt(comments.Count)];
        }

        private static string UnsubscribeCategoryComment(Video video)
        {
            List<string> comments = new List<string>();

            comments.Add(String.Format(CommentStrings.UnsubscribeCategoryComment1.Translate(), video.Category.GetString()));
            comments.Add(String.Format(CommentStrings.UnsubscribeCategoryComment2.Translate(), video.Category.GetString()));
            comments.Add(CommentStrings.UnsubscribeCategoryComment3.Translate());
            comments.Add(CommentStrings.UnsubscribeCategoryComment4.Translate());
            comments.Add(CommentStrings.UnsubscribeCategoryComment5.Translate());

            return comments[RandomHelpers.RandomInt(comments.Count)];
        }

        private static string RandomComment(Video video)
        {
            List<string> comments = new List<string>(RandomComments.Value);

            switch (video.Category)
            {
                case (VideoCategory.Accidents):
                    comments.AddRange(AccidentComments.Value);
                    break;

                case (VideoCategory.AnimalVSAnimal):
                    comments.AddRange(AnimalVsAnimalComments.Value);
                    break;

                case (VideoCategory.Cats):
                    comments.AddRange(CatsComments.Value);
                    break;

                case (VideoCategory.Comedy):
                    comments.AddRange(ComedyComments.Value);
                    break;

                case (VideoCategory.ConspiraryTheories):
                    comments.AddRange(ConspiraryTheoryComments.Value);
                    break;

                case (VideoCategory.Gaming):
                    comments.AddRange(GamingComments.Value);
                    break;

                case (VideoCategory.Hauls):
                    comments.AddRange(HaulComments.Value);
                    break;

                case (VideoCategory.HowTo):
                    comments.AddRange(HowToComments.Value);
                    break;

                case (VideoCategory.MusicVideo):
                    comments.AddRange(MusicVideoComments.Value);
                    break;

                case (VideoCategory.NonProfit):
                    comments.AddRange(NonProfitComments.Value);
                    break;

                case (VideoCategory.Sports):
                    comments.AddRange(SportComments.Value);
                    break;

                case (VideoCategory.TheWeirdSide):
                    comments.AddRange(TheWeirdSideComments.Value);
                    break;

                case (VideoCategory.Technology):
                    comments.AddRange(TechnologyComments.Value);
                    break;

                case (VideoCategory.UkuleleCover):
                    comments.AddRange(UkuleleCoverComments.Value);
                    break;

                case (VideoCategory.Vlog):
                    comments.AddRange(VlogComments.Value);
                    break;

                case (VideoCategory.Anime):
                    comments.AddRange(AnimeComments.Value);
                    break;

                case (VideoCategory.Movies):
                    comments.AddRange(MovieComments.Value);
                    break;

                case (VideoCategory.Creepypasta):
                    comments.AddRange(CreepypastaComments.Value);
                    break;
            }

            if (!String.IsNullOrEmpty(video.YouTubeVideoId) && _videoComments.ContainsKey(video.YouTubeVideoId))
            {
                var realComments = _videoComments[video.YouTubeVideoId];
                if (realComments != null)
                {
                    if (realComments.Count > 30)
                        comments.Clear();
                    comments.AddRange(realComments);
                }
            }

            return comments[RandomHelpers.RandomInt(comments.Count)];
        }
    }
}