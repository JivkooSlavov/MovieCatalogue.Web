using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Common
{
    public static class EntityValidationConstants
    {
        public static class MovieConstants
        {
            public const int MovieTitleMaxLength = 256;
            public const int MovieTitleMinLength = 1;

            public const int MovieDescriptionsMaxLength = 1000;
            public const int MovieDescriptionMinLength = 50;

            public const string DateFormatOfMovie = "dd-MM-yyyy";

            public const int MovieCastMaxLength = 500;
            public const int MovieCastMinLength = 50;

            public const int MovieDurationMaxLength = 999;
            public const int MovieDurationMinLength = 1;

            public const int MovieDirectorMaxLength = 50;
            public const int MovieDirectorMinLength = 5;

            public const int MovieImageUrlMinLength = 8;
            public const int MovieImageUrlMaxLength = 2048;

            public const int MovieRatingDefault = 0;
        }

        public static class RatingConstants
        {
            public const int RatingValueMax = 5;
            public const int RatingValueMin = 1;
        }
        public static class ReviewConstants
        {
            public const int ReviewContentMax = 1000;
            public const int ReviewContentMin = 5;
        }

        public static class GenreConstants
        {
            public const int GenreNameMaxLength = 30;
            public const int GenreNameMinLength = 5;



        }
    }
}
