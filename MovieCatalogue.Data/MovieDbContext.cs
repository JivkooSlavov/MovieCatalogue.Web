using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MovieCatalogue.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogue.Data
{
    public class  MovieDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public MovieDbContext()
        {

        }

        public MovieDbContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Genre>()
                .HasData(
                    new Genre { Id = 1, Name = "Drama" },
                    new Genre { Id = 2, Name = "Comedy" },
                    new Genre { Id = 3, Name = "Action" },
                    new Genre { Id = 4, Name = "Adventure" },
                    new Genre { Id = 5, Name = "Horror" },
                    new Genre { Id = 6, Name = "Triller" },
                    new Genre { Id = 7, Name = "Mystery" },
                     new Genre { Id = 8, Name = "Musical" },
                    new Genre { Id = 9, Name = "Animation" },
                    new Genre { Id = 10, Name = "Fantasy" });
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}