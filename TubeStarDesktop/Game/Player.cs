using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TubeStar
{
    public class Player
    {
        private static Player _current;
        public static Player Current
        {
            get
            {
                if (_current == null)
                    _current = new Player();
                return _current;
            }
            set { _current = value; }
        }

        public event Action MoneyChanged;

        private double _money;
        public double Money 
        {
            get { return _money; }
            set
            {
                _money = value;
                if (MoneyChanged != null)
                {
                    MoneyChanged();
                }
            }
        }

        public bool QuitJob { get; set; }
        public bool HasPromotion { get; set; }
        public bool Overtime { get; set; }

        public List<Channel> Channels { get; set; }
        public List<Task> TasksInProgress { get; set; }
        public List<Video> Videos { get; set; }

        public int ShootingSkill { get; set; }
        public int PostProductionSkill { get; set; }
        public int VideoAttributePoints { get; set; }
        public bool CanViewQualityBeforeUpload { get; set; }

        public int Iterations { get; set; }

        public int CostOfLivingExtra { get; set; }

        public bool ChallengeMode { get; set; }
        public bool UltraMode { get; set; }
        public bool RobotRulers { get; set; }

        public double LoanPayOff { get; set; }

        public Player()
        {
            Reset();
        }

        public void Reset()
        {
            RobotRulers = false;
            Iterations = -1;
            Money = 950;
            QuitJob = false;
            ShootingSkill = 30;
            PostProductionSkill = 20;
            VideoAttributePoints = 2;
            CanViewQualityBeforeUpload = false;
            CostOfLivingExtra = 0;
            Overtime = false;
            HasPromotion = false;
            LoanPayOff = 0;

            TasksInProgress = new List<Task>();
            Videos = new List<Video>();
            Channel.UnreleasedVideos.Name = EnglishStrings.UnreleasedVideos.Translate();
            Channels = new List<Channel>() { Channel.UnreleasedVideos }; //Default channel

            Studies.Current = null;
            StoreItems.Current = null;
            Rivals.Current = null;
        }
    }
}