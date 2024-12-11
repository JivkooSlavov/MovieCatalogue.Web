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
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(f => f.Id);

            builder.HasOne(f => f.Movie)
                .WithMany(m => m.Favorites)
                .HasForeignKey(f => f.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(SeedFavorite());
        }
        //private HashSet<Favorite> SeedFavorite()
        //{
        //    HashSet<Favorite> favorites = new HashSet<Favorite>()
        //    {
        //            new Favorite() { Id = Guid.Parse("6bde756c-dd87-48fe-a4de-7ee9a78779e4"), MovieId=Guid.Parse("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9") },
        //            new Favorite() { Id = Guid.Parse("82ca617c-5ad6-4487-943e-1caf0eea2a37"), MovieId=Guid.Parse("d4e6bf33-5a79-490a-89d1-dc9e2fe12b5c"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9") },
        //            new Favorite() { Id = Guid.Parse("adaafca9-2e3e-4853-9c34-67d511cf2e40"), MovieId=Guid.Parse("000a7d61-c851-4705-9e18-f16ea9624cf7"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016") },
        //    };
        //    return favorites;
        //}
    }
}
