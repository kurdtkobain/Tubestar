using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival3 : Rival
    {
        public Rival3()
            : base()
        {
            Name = RivalStrings.Rival3Name.Translate();
            ShootingSkill = 30;
            PostProductionSkill = 25;
            InitialSubscribers = 150;
            VideoKeyword1 = RivalStrings.Rival3Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival3Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival3Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival3Keyword4.Translate();
        }
    }
}