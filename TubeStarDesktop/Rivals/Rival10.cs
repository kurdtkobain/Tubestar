using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival10 : Rival
    {
        public Rival10()
            : base()
        {
            Name = RivalStrings.Rival10Name.Translate();
            ShootingSkill = 50;
            PostProductionSkill = 40;
            InitialSubscribers = 12000;
            VideoKeyword1 = RivalStrings.Rival10Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival10Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival10Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival10Keyword4.Translate();
        }
    }
}