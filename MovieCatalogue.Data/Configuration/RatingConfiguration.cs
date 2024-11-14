using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using static MovieCatalogue.Common.EntityValidationConstants.Rating;

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
                .HasMaxLength(RatingValueMax);

            builder.HasOne(r => r.Movie)
                .WithMany(m => m.Ratings)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Ratings)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
