using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MovieCatalogue.Data.Models;
using static MovieCatalogue.Common.EntityValidationConstants.ReviewConstants;

namespace MovieCatalogue.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Content)
                .IsRequired()
                .HasMaxLength(ReviewContentMax);

            builder.Property(r => r.DatePosted)
                .IsRequired();

            builder.Property(r => r.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasOne(r => r.Movie)
                .WithMany(m => m.Reviews)
                .HasForeignKey(r => r.MovieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.HasData(SeedReviews());
        }

        //private HashSet<Review> SeedReviews()
        //{
        //    HashSet<Review> reviews = new HashSet<Review>()
        //    {
        //            new Review ()  { Id =Guid.Parse("929d5fb3-82f0-4065-92c0-7cb768d9a617"), Content = "Just watched Harry Potter and the Sorcerer's Stone! Loved the magic and adventure. Can’t wait to see more!",MovieId = Guid.Parse("9a237ae5-bff5-4fa3-b30f-1303ea504903"),
        //                DatePosted = new DateTime(2023, 10, 12, 15, 30, 0), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},
        //            new Review ()  { Id =Guid.Parse("135d2180-da1a-4183-865e-60bee8898f25"), Content = "I was deeply moved by its heartfelt story and Tom Hanks’ incredible performance.",MovieId = Guid.Parse("d4e6bf33-5a79-490a-89d1-dc9e2fe12b5c"),
        //                DatePosted = new DateTime(2024, 11, 11, 19, 30, 0), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
        //            new Review ()  { Id =Guid.Parse("a0906ca2-8c4a-4645-a74e-ef2080bbde6a"), Content = "I love Troy! The epic battles, intense drama, and legendary story kept me hooked from start to finish.",MovieId = Guid.Parse("7dc6c01d-68ed-475c-9ca1-3cda99437db2"),
        //                DatePosted = new DateTime(2024, 09, 09, 17, 30, 0), UserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")},
        //            new Review ()  { Id =Guid.Parse("13123dac-4914-4337-85c7-61bb3bd21947"), Content = "I love Gone Girl! The twists and mind games had me on the edge of my seat the entire time.",MovieId = Guid.Parse("000a7d61-c851-4705-9e18-f16ea9624cf7"),
        //                DatePosted = new DateTime(2022, 12, 12, 12, 12, 0), UserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")},

        //    };
        //    return reviews;
        //}
    }
}