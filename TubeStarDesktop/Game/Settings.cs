namespace TubeStar
{
    public static class Settings
    {
        public static VideoCategory? LastCategory { get; set; }
        public static Channel LastChannel { get; set; }
        public static string PlayerName { get; set; }

        public static string GameJoltLogin { get; set; }
        public static string GameJoltToken { get; set; }

        public static bool ListView { get; set; }
        public static bool UseCreativeCommons { get; set; }

        public static string RivalsModPath { get; set; }
    }
}