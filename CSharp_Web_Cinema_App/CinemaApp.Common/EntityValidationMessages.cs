﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApp.Common
{
    public static class EntityValidationMessages
    {
        public static class Movie
        {
            public const string TitleRequiredMessage = "Movie title ss required";
            public const string GenreRequiredMessage = "Genre is required";
            public const string ReleaseRequiredDateMessage = "Release date is required in format yyyy-MM";
            public const string DirectorRequiredMessage = "Director name is required.";
            public const string DurationRequiredMessage  = "Please specify movie duration.";

        }
    }
}
