namespace CinemaApp.Common
{
    public static class EntityValidationConstants
    {
        public static class Movie 
        {

            public const int TitleMaxLength = 50;
            public const int GenreMinLength = 5;
            public const int GenreMaxLength = 20;
            public const int DirectorNameMinLength = 2;
            public const int DirectorNameMaxLength = 80;
            public const int DescriptionMinLength = 4;
            public const int DescriptionMaxLength = 500;
            public const int DurationMinValue = 5;
            public const int DurationMaxValue = 600;
            public const string ReleaseDateFormat = "yyyy-MM";

        }

        public static class Cinema
        {

            public const int CinemaNameMinLength = 3;
            public const int CinemaNameMaxLength = 50;
            public const int CinemaLocationMinLength = 2;
            public const int CinemaLocationMaxLength = 100;

        }
    }
}
