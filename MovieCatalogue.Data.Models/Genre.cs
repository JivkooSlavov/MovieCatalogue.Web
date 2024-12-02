﻿namespace MovieCatalogue.Data.Models
{
    public class Genre
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}