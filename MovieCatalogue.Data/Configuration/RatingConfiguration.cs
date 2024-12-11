using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using static MovieCatalogue.Common.EntityValidationConstants.RatingConstants;

namespace MovieCatalogue.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Value)
                .IsRequired()
                .HasDefaultValue(RatingValueMin)
                .HasMaxLength(RatingValueMax)
                .HasPrecision(2, 1);

            builder.HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedRating());
        }

        private HashSet<Rating> SeedRating()
        {
            HashSet<Rating> ratings = new HashSet<Rating>()
            {
                    new Rating() { Value =5, MovieId = Guid.Parse("9a237ae5-bff5-4fa3-b30f-1303ea504903"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("b1d07192-c121-443a-8367-b18934739a2e"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("000a7d61-c851-4705-9e18-f16ea9624cf7"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =4, MovieId = Guid.Parse("e8772d7c-8bda-4652-9f91-757857838db7"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =5, MovieId = Guid.Parse("24e4cfc0-ec76-4f80-9d4c-e2f5bba1052e"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =5, MovieId = Guid.Parse("d4e6bf33-5a79-490a-89d1-dc9e2fe12b5c"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =4, MovieId = Guid.Parse("47b1d1ca-756b-4f56-93ad-9d07c44d7b7a"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =4, MovieId = Guid.Parse("31b7d9c1-b9b1-4a3e-9f71-d3d0fdbd94d2"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("7a59a1d2-d249-442a-8fbb-b3b4057e2e9d"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")}
            };

            return ratings;
        }
    }
}
