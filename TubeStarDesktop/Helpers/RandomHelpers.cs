using System;

namespace TubeStar
{
    public static class RandomHelpers
    {
        private const int _videoCategoryLength = 18;
        private static Lazy<Random> _random;

        static RandomHelpers()
        {
            _random = new Lazy<Random>();
        }

        public static VideoCategory RandomVideoCategory
        {
            get { return (VideoCategory)_random.Value.Next(1, _videoCategoryLength + 1); }
        }

        public static int RandomInt(int max)
        {
            return _random.Value.Next(max);
        }

        public static int RandomInt(int min, int max)
        {
            return _random.Value.Next(min, max);
        }

        public static bool Chance(int percentage = 100)
        {
            percentage = Math.Min(percentage, 100);
            percentage = Math.Max(percentage, 0);

            return _random.Value.Next(100) <= percentage;
        }

        public static bool RandomBool()
        {
            return _random.Value.NextDouble() > 0.5;
        }
    }
}