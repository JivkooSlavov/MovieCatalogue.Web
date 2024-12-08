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
            HashSet<Rating> genres = new HashSet<Rating>()
            {
                    new Rating() { Value =5, MovieId = Guid.Parse("9a237ae5-bff5-4fa3-b30f-1303ea504903"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("b1d07192-c121-443a-8367-b18934739a2e"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("000a7d61-c851-4705-9e18-f16ea9624cf7"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =4, MovieId = Guid.Parse("e8772d7c-8bda-4652-9f91-757857838db7"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =5, MovieId = Guid.Parse("d17c51a2-4677-47c9-8f15-1f768b3b7e7b"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =5, MovieId = Guid.Parse("a51ec6e9-e9c2-42a7-a785-1b6a6822d5fa"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =5, MovieId = Guid.Parse("3a8bc77f-6d53-4dc2-9532-8d3fc6a9a4ed"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
                    new Rating() { Value =5, MovieId = Guid.Parse("89d143c2-41f5-4e49-b9b8-3152df7c2f8e"), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
                    new Rating() { Value =4, MovieId = Guid.Parse("4e2c34e7-5b85-4a72-a894-8e71bb045c6e"), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")}
            };

            return genres;
        }
    }
}
