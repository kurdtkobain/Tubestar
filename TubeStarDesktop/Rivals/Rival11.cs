using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TubeStar
{
    public class Rival11 : Rival
    {
        public Rival11()
            : base()
        {
            Name = RivalStrings.Rival11Name.Translate();
            ShootingSkill = 55;
            PostProductionSkill = 40;
            InitialSubscribers = 25000;
            VideoKeyword1 = RivalStrings.Rival11Keyword1.Translate();
            VideoKeyword2 = RivalStrings.Rival11Keyword2.Translate();
            VideoKeyword3 = RivalStrings.Rival11Keyword3.Translate();
            VideoKeyword4 = RivalStrings.Rival11Keyword4.Translate();
        }
    }
}