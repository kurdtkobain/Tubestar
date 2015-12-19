using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace TubeStar
{
    public enum RandomEvent
    {
        VideoSuspended = 1,
        ChannelSuspended = 2,
        LoseSubscribers = 3,
        CostOfLivingIncrease = 4,
        LoseJob = 5,
        Overtime = 6,
        ComputerCrash = 7,

        Promotion = 8,
        RobotsRise = 9,
        FoundSmallChange = 10,
        BeautyContest = 11,

        GainSubscribers = 12,
        CostOfLivingDrops = 13,
        ChannelSuspensionLifted = 14,
        VideoSuspensionLifted = 15,
        FreeClass = 16,
    }

    public static class RandomEvents
    {
        private static int _beautyCount;

        public static void RunEvent()
        {
            if (RandomHelpers.Chance(25))
            {
                RandomEvent randomEvent = (RandomEvent)(RandomHelpers.RandomInt(Enum.GetValues(typeof(RandomEvent)).Length) + 1);

                switch (randomEvent)
                {
                    case RandomEvent.ChannelSuspended:
                        {
                            if (!Player.Current.ChallengeMode)
                            {
                                break;
                            }

                            List<Channel> validChannels = new List<Channel>();
                            foreach (var channel in Player.Current.Channels)
                            {
                                if (channel != Channel.UnreleasedVideos && !channel.IsSuspended)
                                {
                                    validChannels.Add(channel);
                                }
                            }
                            if (validChannels.Count > 0)
                            {
                                var randomChannel = validChannels[RandomHelpers.RandomInt(validChannels.Count)];
                                randomChannel.IsSuspended = true;
                                foreach (var video in randomChannel.Videos)
                                {
                                    video.IsSuspended = true;
                                }
                                ShowMessage(String.Format(EnglishStrings.ChannelSuspended.Translate(), randomChannel.Name), MessagePicture.Static);

                                if (!TrophyManager.HasTrophy(Trophy.DownTheTubes) && randomChannel.Videos.Count >= 10)
                                    TrophyManager.UnlockTrophy(Trophy.DownTheTubes);
                            }
                        }
                        break;
                    case RandomEvent.ComputerCrash:
                        {
                            if (!Player.Current.ChallengeMode && RandomHelpers.RandomBool())
                            {
                                break;
                            }

                            if (Channel.UnreleasedVideos.Videos.Count > 0)
                            {
                                Channel.UnreleasedVideos.Videos.Clear();
                                Player.Current.Videos.Clear();
                                ShowMessage(EnglishStrings.ComputerCrash.Translate(), MessagePicture.Static);
                            }
                        }
                        break;
                    case RandomEvent.CostOfLivingIncrease:
                        {
                            Player.Current.CostOfLivingExtra += 20;
                            ShowMessage(EnglishStrings.CostOfLivingIncrease.Translate(), MessagePicture.Money);
                        }
                        break;
                    case RandomEvent.LoseJob:
                        {
                            if (Player.Current.ChallengeMode && !Player.Current.QuitJob)
                            {
                                Player.Current.QuitJob = true;
                                ShowMessage(EnglishStrings.Fired.Translate(), MessagePicture.Axe);

                                if (!TrophyManager.HasTrophy(Trophy.GiveAndTake) && Player.Current.HasPromotion)
                                    TrophyManager.UnlockTrophy(Trophy.GiveAndTake);
                            }
                        }
                        break;
                    case RandomEvent.LoseSubscribers:
                        {
                            var total = 0;
                            List<Channel> validChannels = new List<Channel>();
                            foreach (var channel in Player.Current.Channels)
                            {
                                if (channel != Channel.UnreleasedVideos && channel.Subscribers > 0)
                                {
                                    int loss = RandomHelpers.RandomInt(Math.Max(1, (int)(channel.Subscribers * 0.2)));
                                    total += loss;
                                    channel.Subscribers -= loss;
                                }
                            }
                            if (total > 0)
                            {
                                ShowMessage(String.Format(EnglishStrings.LoseSubscribers.Translate(), total.ToNumberString()), MessagePicture.Sad);
                            }
                        }
                        break;
                    case RandomEvent.Overtime:
                        {
                            if (!Player.Current.QuitJob)
                            {
                                Player.Current.Overtime = true;
                                ShowMessage(EnglishStrings.WorkOvertime.Translate(), MessagePicture.Work);
                            }
                        }
                        break;
                    case RandomEvent.VideoSuspended:
                        {
                            if (!Player.Current.ChallengeMode && RandomHelpers.RandomBool())
                            {
                                break;
                            }

                            List<Video> validVideos = new List<Video>();
                            foreach (var channel in Player.Current.Channels)
                            {
                                if (channel != Channel.UnreleasedVideos && !channel.IsSuspended)
                                {
                                    foreach (var video in channel.Videos)
                                    {
                                        if (!video.IsSuspended && !video.Attributes.Contains(VideoAttributes.AboveBoard))
                                        {
                                            validVideos.Add(video);
                                        }
                                    }
                                }
                            }
                            if (validVideos.Count > 0)
                            {
                                int randomVideo = RandomHelpers.RandomInt(validVideos.Count);
                                validVideos[randomVideo].IsSuspended = true;
                                ShowMessage(String.Format(EnglishStrings.VideoSuspended.Translate(), validVideos[randomVideo].Name), MessagePicture.Static);
                            }
                        }
                        break;
                    case RandomEvent.FoundSmallChange:
                        {
                            double change = (double)RandomHelpers.RandomInt(500) / 100;
                            if (change > 0)
                            {
                                Player.Current.Money += change;
                                ShowMessage(String.Format(EnglishStrings.SpareChange.Translate(), change.ToCurrencyString()), MessagePicture.Money);
                            }
                        }
                        break;
                    case RandomEvent.Promotion:
                        {
                            if (!Player.Current.QuitJob && !Player.Current.HasPromotion)
                            {
                                Player.Current.HasPromotion = true;
                                ShowMessage(EnglishStrings.Promoted.Translate(), MessagePicture.Work);
                            }
                        }
                        break;
                    case RandomEvent.RobotsRise:
                        {
                            if (!Player.Current.RobotRulers)
                            {
                                Player.Current.RobotRulers = true;
                                ShowMessage(EnglishStrings.RobotRising.Translate(), MessagePicture.Robot);

                                if (!TrophyManager.HasTrophy(Trophy.RobotMasters))
                                    TrophyManager.UnlockTrophy(Trophy.RobotMasters);
                            }
                            else
                            {
                                Player.Current.Money -= (Player.Current.Money * 0.2);
                                ShowMessage(EnglishStrings.RobotTax.Translate(), MessagePicture.Robot);
                            }
                        }
                        break;
                    case RandomEvent.BeautyContest:
                        {
                            _beautyCount++;
                            Player.Current.Money += 200;
                            ShowMessage(String.Format(EnglishStrings.BeautyContest.Translate(), (200).ToCurrencyString()), MessagePicture.Money);

                            if (!TrophyManager.HasTrophy(Trophy.TopModel) && _beautyCount >= 3)
                                TrophyManager.UnlockTrophy(Trophy.TopModel);
                        }
                        break;
                    case RandomEvent.ChannelSuspensionLifted:
                        {
                            List<Channel> validChannels = new List<Channel>();
                            foreach (var channel in Player.Current.Channels)
                            {
                                if (channel != Channel.UnreleasedVideos && channel.IsSuspended)
                                {
                                    validChannels.Add(channel);
                                }
                            }
                            if (validChannels.Count > 0)
                            {
                                var randomChannel = validChannels[RandomHelpers.RandomInt(validChannels.Count)];
                                randomChannel.IsSuspended = false;
                                foreach (var video in randomChannel.Videos)
                                {
                                    video.IsSuspended = false;
                                }
                                ShowMessage(String.Format(EnglishStrings.ChannelSuspensionLifted.Translate(), randomChannel.Name), MessagePicture.Happy);
                            }
                        }
                        break;
                    case RandomEvent.CostOfLivingDrops:
                        {
                            Player.Current.CostOfLivingExtra -= 20;
                            ShowMessage(EnglishStrings.CostOfLivingDecreased.Translate(), MessagePicture.Money);
                        }
                        break;
                    case RandomEvent.FreeClass:
                        {
                            Study chosenStudy = null;
                            foreach (var study in Studies.Current.All)
                            {
                                if (!study.IsCompleted && (study.Prerequisite == null || study.Prerequisite.IsCompleted))
                                {
                                    if (Player.Current.TasksInProgress.Count(t => t.Name == study.Name) == 0)
                                    {
                                        if (chosenStudy == null || study.Cost < chosenStudy.Cost)
                                            chosenStudy = study;
                                    }
                                }
                            }

                            if (chosenStudy != null)
                            {
                                Player.Current.TasksInProgress.Add(chosenStudy);
                                ShowMessage(String.Format(EnglishStrings.FreeStudy.Translate(), chosenStudy.Name), MessagePicture.Study);
                            }
                        }
                        break;
                    case RandomEvent.GainSubscribers:
                        {
                            var total = 0;
                            List<Channel> validChannels = new List<Channel>();
                            foreach (var channel in Player.Current.Channels)
                            {
                                if (channel != Channel.UnreleasedVideos)
                                {
                                    int gain = RandomHelpers.RandomInt(Math.Max(1, (int)(channel.Subscribers * 0.2)));
                                    total += gain;
                                    channel.Subscribers += gain;
                                }
                            }
                            if (total > 0)
                            {
                                ShowMessage(String.Format(EnglishStrings.GainSubscribers.Translate(), total.ToNumberString()), MessagePicture.Happy);
                            }
                        }
                        break;
                    case RandomEvent.VideoSuspensionLifted:
                        {
                            List<Video> validVideos = new List<Video>();
                            foreach (var channel in Player.Current.Channels)
                            {
                                if (channel != Channel.UnreleasedVideos && !channel.IsSuspended)
                                {
                                    foreach (var video in channel.Videos)
                                    {
                                        if (video.IsSuspended)
                                        {
                                            validVideos.Add(video);
                                        }
                                    }
                                }
                            }
                            if (validVideos.Count > 0)
                            {
                                int randomVideo = RandomHelpers.RandomInt(validVideos.Count);
                                validVideos[randomVideo].IsSuspended = false;
                                ShowMessage(String.Format(EnglishStrings.VideoSuspensionLifted.Translate(), validVideos[randomVideo].Name), MessagePicture.Happy);
                            }
                        }
                        break;
                }
            }
        }

        private static void ShowMessage(string message, MessagePicture picture)
        {
            CustomMessageBox.ShowDialog(EnglishStrings.RandomEvent.Translate(), message, picture);
        }
    }
}