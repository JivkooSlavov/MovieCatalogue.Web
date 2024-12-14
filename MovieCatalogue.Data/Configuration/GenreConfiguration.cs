using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
                    new Genre() { Id = Guid.Parse("c2e80bd4-44f7-42f2-8a76-189fe9d12754"), Name = "Drama" },
                    new Genre() { Id = Guid.Parse("5a5e5959-17ee-4b2a-8902-497a1c79321e"), Name = "Comedy" },
                    new Genre() { Id = Guid.Parse("d4a34098-333d-4177-9cbc-53a979f0382c"), Name = "Action" },
                    new Genre() { Id = Guid.Parse("6f7c25cb-88f7-4036-902e-473b8b7f06d0"), Name = "Adventure" },
                    new Genre() { Id = Guid.Parse("a8dbee99-43f7-4df1-824b-4b27600f67bd"), Name = "Horror" },
                    new Genre() { Id = Guid.Parse("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"), Name = "Triller" },
                    new Genre() { Id = Guid.Parse("e692d3ba-17f1-466d-b8e5-aac14c4d8242"), Name = "Mystery" },
                    new Genre() { Id = Guid.Parse("d5ce01d4-9ac0-411c-b813-e022d6886652"), Name = "History" },
                    new Genre() { Id = Guid.Parse("ff41706e-522a-47cf-93e3-e646823ea7ed"), Name = "Animation" },
                    new Genre() { Id = Guid.Parse("93df566f-30f5-4d2b-b6ff-dc540b86a289"), Name = "Fantasy" },
            };

            return genres;
        }
    }
}