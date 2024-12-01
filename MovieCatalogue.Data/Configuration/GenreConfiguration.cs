﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using static MovieCatalogue.Common.EntityValidationConstants.GenreConstants;
using System.Reflection.Emit;
namespace MovieCatalogue.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(GenreNameMaxLength);

            builder.HasData(SeedGenres());
        }
        private HashSet<Genre> SeedGenres()
        {
            HashSet<Genre> genres = new HashSet<Genre>()
            {
                    new Genre() { Id = 1, Name = "Drama" },
                    new Genre() { Id = 2, Name = "Comedy" },
                    new Genre() { Id = 3, Name = "Action" },
                    new Genre() { Id = 4, Name = "Adventure" },
                    new Genre() { Id = 5, Name = "Horror" },
                    new Genre() { Id = 6, Name = "Triller" },
                    new Genre() { Id = 7, Name = "Mystery" },
                    new Genre() { Id = 8, Name = "History" },
                    new Genre() { Id = 9, Name = "Animation" },
                    new Genre() { Id = 10, Name = "Fantasy" },
            };

            return genres;
        }
    }
}
