using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival1 : Rival
    {
        public Rival1()
            : base()
        {
            Name = RivalStrings.Rival1Name.Translate();
            ShootingSkill = 20;
            PostProductionSkill = 20;
            InitialSubscribers = 20;
            VideoKeyword1 = RivalStrings.Rival1Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival1Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival1Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival1Keyword4.Translate();
        }
    }
}