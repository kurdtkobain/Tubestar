using System;

namespace TubeStar
{
    public static class VideoAttributes
    {
        public static SoBad SoBad { get; private set; }
        public static SecondTime SecondTime { get; private set; }
        public static ProductPlacement ProductPlacement { get; private set; }
        public static Memetic Memetic { get; private set; }
        public static LearnFromMistakes LearnFromMistakes { get; private set; }
        public static Hypnotic Hypnotic { get; private set; }
        public static GenreBuster GenreBuster { get; private set; }
        public static Copycat CopyCat { get; private set; }
        public static FanboyBait FanBoyBait { get; private set; } 
        public static Cats Cats { get; private set; }
        public static Crowdfunding Crowdfunding { get; private set; }
        public static AboveBoard AboveBoard { get; private set; } 

        static VideoAttributes()
        {
            SoBad = new SoBad();
            SecondTime = new SecondTime();
            ProductPlacement = new ProductPlacement();
            Memetic = new Memetic();
            LearnFromMistakes = new LearnFromMistakes();
            Hypnotic = new Hypnotic();
            GenreBuster = new GenreBuster();
            CopyCat = new Copycat();
            FanBoyBait = new FanboyBait();
            Cats = new Cats();
            Crowdfunding = new Crowdfunding();
            AboveBoard = new AboveBoard();
        }
    }
}