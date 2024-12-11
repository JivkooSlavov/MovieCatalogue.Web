using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using System.Reflection;

namespace MovieCatalogue.Data
{
    public class MovieDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            var passwordHasher = new PasswordHasher<User>();
            var testUser = new User
            {
                Id = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                UserName = "zhivko@movie.com",
                NormalizedUserName = "ZHIVKO@MOVIE.COM",
                Email = "zhivko@movie.com",
                NormalizedEmail = "ZHIVKO@MOVIE.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            testUser.PasswordHash = passwordHasher.HashPassword(testUser, "123456");

            modelBuilder.Entity<User>().HasData(testUser);

            var passwordHasher1 = new PasswordHasher<User>();
            var testUser1 = new User
            {
                Id = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016"),
                UserName = "mitko@movie.com",
                NormalizedUserName = "MITKO@MOVIE.COM",
                Email = "mitko@movie.com",
                NormalizedEmail = "MITKO@MOVIE.COM",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            testUser1.PasswordHash = passwordHasher1.HashPassword(testUser1, "123456");

            modelBuilder.Entity<User>().HasData(testUser1);

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}