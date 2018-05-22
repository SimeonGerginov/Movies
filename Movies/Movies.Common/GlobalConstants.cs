namespace Movies.Common
{
    public static class GlobalConstants
    {
        // Admin Info
        public const string AdminRole = "Admin";

        // Movie Constants
        public const int MinMovieLength = 3;
        public const int MaxMovieLength = 40;
        public const int MovieYearLength = 4;
        public const int MinMovieRunningTime = 10;
        public const int MaxMovieRunningTime = 600;
        public const int MovieDescriptionLength = 200;
        public const int MinMovieRating = 1;
        public const int MaxMovieRating = 10;

        // Genre Constants
        public const int MinGenreNameLength = 3;
        public const int MaxGenreNameLength = 20;

        // Person Constants
        public const int MinPersonNameLength = 3;
        public const int MaxPersonNameLength = 30;
        public const int MinPersonNationalityLength = 3;
        public const int MaxPersonNationalityLength = 30;
        public const int MinPersonAge = 1;
        public const int MaxPersonAge = 120;

        // User Constants
        public const int MinUserNameLength = 3;
        public const int MaxUserNameLength = 30;

        // Grids
        public const int GridsPageSize = 12;
    }
}
