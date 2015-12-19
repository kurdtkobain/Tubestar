using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival14 : Rival
    {
        public Rival14()
            : base()
        {
            Name = RivalStrings.Rival14Name.Translate();
            ShootingSkill = 65;
            PostProductionSkill = 50;
            InitialSubscribers = 80000;
            VideoKeyword1 = RivalStrings.Rival14Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival14Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival14Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival14Keyword4.Translate();
        }
    }
}