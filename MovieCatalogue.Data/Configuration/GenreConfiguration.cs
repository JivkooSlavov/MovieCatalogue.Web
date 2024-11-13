using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
        private IEnumerable<Genre> GenerateGenres()
        {
            IEnumerable<Genre> genres = new List<Genre>()
            {
                new Genre()
                {
                    Id = 1,
                    Name = "Drama",
                },
                    new Genre()
                {
                    Id = 2,
                    Name = "Comedy",
                },
                     new Genre()
                {
                         Id = 3,
                    Name = "Action",
                },
                    new Genre()
                {
                        Id = 4,
                    Name = "Adventure",
                },
                     new Genre()
                {
                         Id = 5,
                    Name = "Horror",
                },
                    new Genre()
                {
                        Id = 6,
                    Name = "Triller",
                },
                    new Genre()
                {
                         Id=7,       
                    Name = "Mystery",
                },
                    new Genre()
                {
                        Id = 8,
                    Name = "Musical",
                },
                    new Genre()
                {     Id = 9,
                    Name = "Animation",
                },
                    new Genre()
                {
                        Id = 10,
                    Name = "Fantasy"
                }
            };

            return genres;
        }
    }
}
