using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCatalogue.Data.Models;
using static MovieCatalogue.Common.EntityValidationConstants.Movie;

namespace MovieCatalogue.Data.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(MovieTitleMaxLength);

            builder.Property(m => m.Description)
                .HasMaxLength(MovieDescriptionsMaxLength);

            builder.Property(m => m.ReleaseDate)
                .IsRequired();

            builder.Property(m => m.Cast)
                .HasMaxLength(MovieCastMaxLength);

            builder.Property(m => m.Rating)
                .HasDefaultValue(MovieRatingDefault)
                .HasPrecision(2, 1);

            builder.Property(m => m.PosterUrl)
                .IsRequired()
                .HasMaxLength(MovieImageUrlMaxLength);

            builder.Property(m=>m.Director)
                .IsRequired()
                .HasMaxLength(MovieDirectorMaxLength);

            builder.HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
