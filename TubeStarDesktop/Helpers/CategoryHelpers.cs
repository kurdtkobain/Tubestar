namespace TubeStar
{
    public static class CategoryHelpers
    {
        public static bool CheckInterest(VideoCategory category)
        {
            if (RandomHelpers.RandomVideoCategory == category)
            {
                return true;
            }
            else if (RandomHelpers.RandomVideoCategory == category)
            {
                return RandomHelpers.RandomBool(); //50% chance
            }
            else return RandomHelpers.Chance(5); //5% chance
        }

        public static bool CheckInterest(Video video)
        {
            var result = CheckInterest(video.Category);
            if (!result && video.Attributes.Contains(VideoAttributes.GenreBuster))
            {
                return RandomHelpers.Chance(35); //extra 35% chance
            }
            return result;
        }
    }
}