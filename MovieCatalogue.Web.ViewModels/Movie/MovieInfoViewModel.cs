﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieCatalogue.Data.Models;

namespace MovieCatalogue.Web.ViewModels.Movie
{
    public class MovieInfoViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string Cast { get; set; } = null!;
        public double Rating { get; set; } 
        public string TrailerUrl { get; set; } = null!;
        public string Director { get; set; } = null!;
        public int Duration { get; set; }
        public string PosterUrl { get; set; } = null!;

        public virtual ICollection<Rating> Ratings { get; set; }

    }
}
