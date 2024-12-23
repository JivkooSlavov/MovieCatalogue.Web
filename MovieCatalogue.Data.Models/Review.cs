﻿namespace MovieCatalogue.Data.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime DatePosted { get; set; }
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Guid UserId { get; set; } 
        public User User { get; set; } = null!;
    }
}