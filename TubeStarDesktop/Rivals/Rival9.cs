using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival9 : Rival
    {
        public Rival9()
            : base()
        {
            Name = RivalStrings.Rival9Name.Translate();
            ShootingSkill = 50;
            PostProductionSkill = 35;
            InitialSubscribers = 7000;
            VideoKeyword1 = RivalStrings.Rival9Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival9Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival9Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival9Keyword4.Translate();
        }
    }
}