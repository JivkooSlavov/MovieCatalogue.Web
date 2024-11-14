﻿namespace MovieCatalogue.Data.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}