using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCatalogue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(m => m.Description)
                .HasMaxLength(1000);

            builder.Property(m => m.ReleaseDate)
                .IsRequired();

            builder.Property(m => m.Cast)
                .HasMaxLength(500);

            builder.Property(m => m.Rating)
                .HasDefaultValue(0)
                .HasPrecision(2, 1);

            builder.Property(m => m.TrailerUrl)
                .HasMaxLength(2048);


            builder.HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        private IEnumerable<Movie> GenerateMovies()
        {
            IEnumerable<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                   Id = 1,
                   Title= "Harry Potter and the Philosopher's Stone",
                   Description = "Harry Potter and the Philosopher's Stone (also known as Harry Potter and the Sorcerer's Stone in the United States) is a 2001 fantasy film directed by Chris Columbus and produced by David Heyman from a screenplay by Steve Kloves.",
                   ReleaseDate = DateTime.Now,
                   GenreId = 4,
                   TrailerUrl = "https://www.youtube.com/watch?v=VyHV0BRtdxo"

                },
                new Movie()
                {
                    Id = 2,
                   Title= "Harry Potter and the Chamber of Secrets",
                   Description = " The plot follows Harry's second year at Hogwarts School of Witchcraft and Wizardry, during which a series of messages on the walls of the school's corridors warn that the \"Chamber of Secrets\" has been opened and that the \"heir of Slytherin\" would kill all pupils who do not come from all-magical families.",
                   ReleaseDate = DateTime.Now,
                   GenreId = 4,
                   TrailerUrl = "https://www.youtube.com/watch?v=1bq0qff4iF8"
                }
            };

            return movies;
        }
    }
}
