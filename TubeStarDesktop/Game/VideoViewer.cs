using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TubeStar
{
    public class SerializableShareData
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
    }

    public static class VideoViewer
    {
        private static ConcurrentDictionary<Guid, int> _videoShares;
        private static ConcurrentDictionary<Guid, int> _videoBoughtViews;

        public static List<SerializableShareData> GetShares()
        {
            List<SerializableShareData> data = new List<SerializableShareData>();
            foreach (var item in _videoShares)
            {
                data.Add(new SerializableShareData() { Id = item.Key, Count = item.Value });
            }
            return data;
        }

        public static void SetShares(List<SerializableShareData> data)
        {
            _videoShares.Clear();
            foreach (var item in data)
            {
                _videoShares[item.Id] = item.Count;
            }
        }

        public static List<SerializableShareData> GetBoughtViews()
        {
            List<SerializableShareData> data = new List<SerializableShareData>();
            foreach (var item in _videoBoughtViews)
            {
                data.Add(new SerializableShareData() { Id = item.Key, Count = item.Value });
            }
            return data;
        }

        public static void SetBoughtViews(List<SerializableShareData> data)
        {
            _videoBoughtViews.Clear();
            foreach (var item in data)
            {
                _videoBoughtViews[item.Id] = item.Count;
            }
        }

        static VideoViewer()
        {
            Reset();
        }

        public static void Reset()
        {
            _videoShares = new ConcurrentDictionary<Guid, int>();
            _videoBoughtViews = new ConcurrentDictionary<Guid, int>();
        }

        public static void BuyViews(Video video, int number)
        {
            if (!_videoBoughtViews.ContainsKey(video.Id))
            {
                _videoBoughtViews[video.Id] = 0;
            }

            _videoBoughtViews[video.Id] += number;
        }

        public static void FreeViews(Video video, int number)
        {
            Initialize(video);
            _videoShares[video.Id] += number;
        }

        public static void SubscriberView(Channel channel, Video video)
        {
            Initialize(video);
            _videoShares[video.Id] += (int)(channel.Subscribers * (Player.Current.ChallengeMode ? 0.2 : 0.4));
        }

        public static void Iteration(Channel channel, Video video, ref double dailyIncome, ref double dailyExpenses)
        {
            Initialize(video);
            video.Iterations++;

            if (channel.IsSuspended || video.IsSuspended)
            {
                _videoShares[video.Id] = 0;
                _videoBoughtViews[video.Id] = 0;
                return;
            }

            int tempShares = (int)(_videoShares[video.Id] * (Player.Current.ChallengeMode ? 0.1 : 0.2)); 
            _videoShares[video.Id] -= tempShares;

            int payedViews = _videoBoughtViews.ContainsKey(video.Id) ? _videoBoughtViews[video.Id] : 0;
            _videoBoughtViews[video.Id] = 0;

            var searchEngineViews = video.Views / (50 * video.Iterations);
            var qualityViews = Math.Max(0, ((video.Quality.Value - Math.Pow(video.Iterations, Player.Current.ChallengeMode ? 2 : 1)) * video.Quality.Value) / Math.Max((Player.Current.ChallengeMode ? 2 : 1), (100 - video.Quality.Value) / 2));
            var iterationViews = searchEngineViews + qualityViews + tempShares;

            int skip = 0;
            while (skip <= iterationViews)
            {
                var jump = RandomHelpers.RandomInt(4, 6);
                for (int i = 0; i < jump; i++)
                {
                    ViewVideo(channel, video, ref dailyIncome, ref dailyExpenses, 1);
                }
                skip += jump;
            }

            for (int i = 0; i < payedViews; i++)
            {
                ViewVideo(channel, video, ref dailyIncome, ref dailyExpenses, 1, true);
            }

            if (channel.Subscribers < channel.MinimumSubsribers)
                channel.Subscribers = channel.MinimumSubsribers;
        }

        private static void ViewVideo(Channel channel, Video video, ref double income, ref double expenses, int numViews, bool payedView = false)
        {
            CommentType? commentType = null;

            video.Views += numViews;
            income += ViewIncome(channel, video, payedView, numViews);
            expenses += video.CostPerView * numViews;

            if (video.Attributes.Contains(VideoAttributes.SecondTime) && RandomHelpers.Chance(5))
            {
                int tempViews = RandomHelpers.RandomInt(numViews * 2);
                video.Views += tempViews;
                income += ViewIncome(channel, video, payedView, tempViews);
                expenses += video.CostPerView * numViews;
            }

            if (RandomHelpers.Chance(2))
                commentType = CommentType.Random;

            if (CategoryHelpers.CheckInterest(video))
            {
                if (RandomHelpers.Chance(video.Quality.Value))
                {
                    video.Likes += numViews;

                    if (RandomHelpers.Chance(1))
                        commentType = CommentType.Like;

                    if (GetSubscriptionChance(channel, video))
                    {
                        if (video.Attributes.Contains(VideoAttributes.Crowdfunding))
                        {
                            if (!payedView)
                                Player.Current.Money += numViews * VideoAttributes.Crowdfunding.Money;
                        }
                        else
                        {
                            channel.Subscribers += numViews;

                            if (RandomHelpers.Chance(1))
                                commentType = CommentType.Subscribe;
                        }
                    }

                    ShareVideo(video, numViews);
                }
                else if (RandomHelpers.RandomBool())
                {
                    video.Dislikes += numViews;

                    if (RandomHelpers.Chance(2))
                        commentType = CommentType.Dislike;

                    //Chance of unsubscribe because of dislike
                    if (CheckUnsubscriptions(channel, video, numViews, payedView))
                    {
                        if (RandomHelpers.Chance(1))
                            commentType = CommentType.UnsubscribeQuality;
                    }

                    //Share video if "So bad" attribute on video
                    if (video.Attributes.Contains(VideoAttributes.SoBad))
                    {
                        ShareVideo(video, numViews);
                    }
                }
            }
            else
            {
                //Chance of unsubscribe because of disinterest in category
                if (CheckUnsubscriptions(channel, video, numViews, payedView))
                {
                    if (RandomHelpers.Chance(1))
                        commentType = CommentType.UnsubscribeCategory;
                }
            }

            if (!channel.IsRivalChannel && commentType.HasValue)
            {
                var comment = CommentGenerator.GenerateComment(video, commentType.Value);
                var first = video.Comments.FirstOrDefault(c => c.Comment == comment);
                if (first != null)
                    first.Likes++;
                else
                    video.Comments.Add(new VideoComment(comment, 0));
            }
        }

        private static bool CheckUnsubscriptions(Channel channel, Video video, int numViews, bool payedView)
        {
            if (!channel.IsRivalChannel && !payedView)
            {
                int chance = (channel.Subscribers / video.Views) * 100;
                if (!Player.Current.ChallengeMode) chance = chance / 2;
                if (RandomHelpers.Chance(25) && GetUnsubscriptionChance(channel) && RandomHelpers.Chance(chance))
                {
                    channel.Subscribers -= numViews;
                    return true;
                }
            }
            return false;
        }

        private static void ShareVideo(Video video, int numViews)
        {
            //Like enough to share
            int memeticMutator = video.Attributes.Contains(VideoAttributes.Memetic) ? 20 : 0;
            if (RandomHelpers.RandomInt(100 + memeticMutator) >= 60)
            {
                //Share
                var friends = RandomHelpers.RandomInt(10 * numViews);
                for (int i = 0; i < friends; i++)
                {
                    if (CategoryHelpers.CheckInterest(video))
                    {
                        _videoShares[video.Id] += 1;
                    }
                }

                //Owner of site - share video on site
                if (RandomHelpers.Chance(1) && RandomHelpers.Chance(10))
                {
                    var subscribers = RandomHelpers.RandomInt((int)(25000 * (Player.Current.ChallengeMode ? 0.02 : 0.05)));
                    _videoShares[video.Id] += (int)(subscribers * 0.25);
                }
            }
        }

        private static bool GetSubscriptionChance(Channel channel, Video video)
        {
            int initialChance = 0;
            switch (channel.Advertising)
            {
                case (AdvertisingStrategy.Low):
                    initialChance = 90;
                    break;
                case (AdvertisingStrategy.Normal):
                    initialChance = 70;
                    break;
                case (AdvertisingStrategy.High):
                    initialChance = 50;
                    break;
                case (AdvertisingStrategy.Aggressive):
                    initialChance = 30;
                    break;
                default: throw new NotSupportedException();
            }

            if (video.Attributes.Contains(VideoAttributes.Hypnotic))
                initialChance += 30;

            return RandomHelpers.Chance(initialChance);
        }

        private static bool GetUnsubscriptionChance(Channel channel)
        {
            switch (channel.Advertising)
            {
                case (AdvertisingStrategy.Low): return RandomHelpers.Chance(10);
                case (AdvertisingStrategy.Normal): return RandomHelpers.Chance(20);
                case (AdvertisingStrategy.High): return RandomHelpers.Chance(40);
                case (AdvertisingStrategy.Aggressive): return RandomHelpers.Chance(60);
                default: throw new NotSupportedException();
            }
        }

        private static double ViewIncome(Channel channel, Video video, bool payedView, int numViews)
        {
            //Don't get base view payment for payed views
            var income = payedView ? 0 : channel.IncomePerView;
            if (Player.Current.ChallengeMode)
                income = income / 2;

            var extraIncome = Math.Sqrt(channel.Subscribers / (Player.Current.ChallengeMode ? 20000 : 1000)) * 0.01;
            extraIncome = Math.Min(extraIncome, Player.Current.ChallengeMode ? 0.25 : 0.50);
            income += extraIncome;

            if (Player.Current.ChallengeMode && RandomHelpers.Chance(90))
            {
                if (Player.Current.UltraMode)
                {
                    if (RandomHelpers.Chance(90))
                    {
                        income = 0; //only 10% CTR in ultra mode
                    }
                }
                else
                {
                    if (RandomHelpers.Chance(50))
                    {
                        income = 0; //only 50% CTR in challenge mode
                    }
                }
            }

            if (video.Attributes.Contains(VideoAttributes.ProductPlacement))
            {
                income += 0.01; //Extra 1c
            }

            return income * numViews;
        }

        private static void Initialize(Video video)
        {
            if (!_videoShares.ContainsKey(video.Id))
            {
                _videoShares[video.Id] = 0;
            }
        }
    }
}