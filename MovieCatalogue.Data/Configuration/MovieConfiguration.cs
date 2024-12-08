using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieCatalogue.Data.Models;
using static MovieCatalogue.Common.EntityValidationConstants.MovieConstants;
using static MovieCatalogue.Common.ApplicationConstants;

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

            builder
             .Property(m => m.PosterUrl)
             .IsRequired()
             .HasMaxLength(MovieImageUrlMaxLength);

            builder.Property(m=>m.Director)
                .IsRequired()
                .HasMaxLength(MovieDirectorMaxLength);

            builder
                .Property(m=>m.IsDeleted)
                .HasDefaultValue(false);

            builder.HasOne(m => m.Genre)
                .WithMany(g => g.Movies)
                .HasForeignKey(m => m.GenreId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.CreatedByUser)
                .WithMany(g => g.CreatedMovies)
                .HasForeignKey(m => m.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(SeedMovies());
        }
        private List<Movie> SeedMovies()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie()
                {
                    Id = Guid.Parse("9a237ae5-bff5-4fa3-b30f-1303ea504903"),
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "Harry Potter and the Philosopher's Stone (also known as Harry Potter and the Sorcerer's Stone in the United States) is a 2001 fantasy film directed by Chris Columbus and produced by David Heyman from a screenplay by Steve Kloves. It is based on the 1997 novel Harry Potter and the Philosopher's Stone by J. K. Rowling. It is the first instalment in the Harry Potter film series. The film stars Daniel Radcliffe as Harry Potter, with Rupert Grint as Ron Weasley, and Emma Watson as Hermione Granger. Its story follows Harry's first year at Hogwarts School of Witchcraft and Wizardry as he discovers that he is a famous wizard and begins his formal wizarding education.",
                    Director = "Chris Columbus",
                    Cast = "Daniel Radcliffe, Rupert Grint, Emma Watson, John Cleese, Robbie Coltrane, Warwick Davis, Richard Griffiths, Richard Harris, Ian Hart, John Hurt, Alan Rickman, Fiona Shaw, Maggie Smith, Julie Walters",
                    PosterUrl = "https://img.posterstore.com/zoom/wb0101-8harrypotter-thephilosophersstoneno150x70.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=VyHV0BRtdxo",
                    Duration = 152,
                    GenreId = Guid.Parse("6f7c25cb-88f7-4036-902e-473b8b7f06d0"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2001, 11, 11),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                },
               new Movie()
                {
                   Id=Guid.Parse("b1d07192-c121-443a-8367-b18934739a2e"),
                    Title = "The Hobbit: An Unexpected Journey",
                    Description = "The story is set in Middle-earth sixty years before the main events of The Lord of the Rings and portions of the film are adapted from the appendices to Tolkien's The Return of the King.[7] An Unexpected Journey tells the tale of Bilbo Baggins (Martin Freeman), who is convinced by the wizard Gandalf (Ian McKellen) to accompany thirteen Dwarves, led by Thorin Oakenshield (Richard Armitage), on a quest to reclaim the Lonely Mountain from the dragon Smaug. The ensemble cast also includes Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, James Nesbitt, Elijah Wood, and Andy Serkis. It features Sylvester McCoy, Barry Humphries, and Manu Bennett.",
                    Director = "Peter Jackson",
                    Cast = "Ian McKellen, Martin Freeman, Richard Armitage, James Nesbitt, Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, Elijah Wood, Andy Serkis",
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTcwNTE4MTUxMl5BMl5BanBnXkFtZTcwMDIyODM4OA@@._V1_FMjpg_UX1000_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=9PSXjr1gbjc",
                    Duration = 169,
                    GenreId = Guid.Parse("6f7c25cb-88f7-4036-902e-473b8b7f06d0"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2012, 11, 28),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                },
               new Movie()
                { Id = Guid.Parse("000a7d61-c851-4705-9e18-f16ea9624cf7"),
                    Title = "Gone Girl",
                    Description = "Gone Girl is aAmerican psychological thriller film directed by David Fincher and written by Gillian Flynn, based on her 2012 novel of the same name. It stars Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry, and Carrie Coon in her film debut. In the film, Nick Dunne (Affleck) becomes the prime suspect in the sudden disappearance of his wife, Amy (Pike) in Missouri.",
                    Director = "David Fincher",
                    Cast = "Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry",
                    PosterUrl = "https://m.media-amazon.com/images/I/61cdYGoHFrL._AC_SY879_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=2-_-1nJf8Vg",
                    Duration = 149,
                    GenreId = Guid.Parse("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2014, 09, 26),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                },
               new Movie()
                {  Id = Guid.Parse("7dc6c01d-68ed-475c-9ca1-3cda99437db2"),
                    Title = "Troy",
                    Description = "Troy is a 2004 epic historical war film directed by Wolfgang Petersen and written by David Benioff. Produced by units in Malta, Mexico and Britain's Shepperton Studios, the film features an ensemble cast led by Brad Pitt, Eric Bana, Sean Bean, Diane Kruger, Brian Cox, Brendan Gleeson, Rose Byrne, Saffron Burrows and Orlando Bloom. It is loosely based[3] on Homer's Iliad in its narration of the entire story of the decade-long Trojan War—condensed into little more than a couple of weeks, rather than just the quarrel between Achilles and Agamemnon in the ninth year. Achilles leads his Myrmidons along with the rest of the Greek army invading the historical city of Troy, defended by Hector's Trojan army. The end of the film (the sack of Troy) is not taken from the Iliad, but rather from Quintus Smyrnaeus's Posthomerica, as the Iliad concludes with Hector's death and funeral.",
                    Director = "Wolfgang Petersen",
                    Cast = "Brad Pitt, Eric Bana, Orlando Bloom, Diane Kruger, Brian Cox, Sean Bean, Brendan Gleeson, Peter O'Toole",
                    PosterUrl = "https://www.rogerebert.com/wp-content/uploads/2024/03/Troy.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=znTLzRJimeY",
                    Duration = 183,
                    GenreId = Guid.Parse("d5ce01d4-9ac0-411c-b813-e022d6886652"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2004, 05, 14),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
               new Movie()
                { Id = Guid.Parse("e8772d7c-8bda-4652-9f91-757857838db7"),
                    Title = "Ice Age",
                    Description = "On Earth 20,000 years ago, everything was covered in ice. A group of friends, Manny, a mammoth, Diego, a saber tooth tiger, and Sid, a sloth encounter an Eskimo human baby. They must try to return the baby back to his tribe before a group of saber tooth tigers find him and eat him.",
                    Director = "Chris Wedge",
                    Cast = "Ray Romano, John Leguizamo, Denis Leary",
                    PosterUrl = "https://image.tmdb.org/t/p/original/idg7vYulQRXKEHfLIM0XKtqPkPz.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=wjdqn9r4thg",
                    Duration = 81,
                    GenreId = Guid.Parse("ff41706e-522a-47cf-93e3-e646823ea7ed"),
                    Rating = 4,
                    ReleaseDate = new DateTime(2003, 04, 04),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
               new Movie()
{
                    Id = Guid.Parse("d17c51a2-4677-47c9-8f15-1f768b3b7e7b"),
                    Title = "The Shawshank Redemption",
                    Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                    Director = "Frank Darabont",
                    Cast = "Tim Robbins, Morgan Freeman, Bob Gunton",
                    PosterUrl = "https://m.media-amazon.com/images/I/51NiGlapXlL._AC_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=NmzuHjWmXOc",
                    Duration = 142,
                    GenreId = Guid.Parse("c2e80bd4-44f7-42f2-8a76-189fe9d12754"),
                    Rating = 5,
                    ReleaseDate = new DateTime(1994, 09, 22),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
               new Movie()
{
                    Id = Guid.Parse("a51ec6e9-e9c2-42a7-a785-1b6a6822d5fa"),
                    Title = "The Dark Knight",
                    Description = "When the menace known as the Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham. The Dark Knight must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                    Director = "Christopher Nolan",
                    Cast = "Christian Bale, Heath Ledger, Aaron Eckhart",
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY",
                    Duration = 152,
                    GenreId = Guid.Parse("d4a34098-333d-4177-9cbc-53a979f0382c"), 
                    Rating = 5,
                    ReleaseDate = new DateTime(2008, 07, 18),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
                new Movie()
                {
                    Id = Guid.Parse("3a8bc77f-6d53-4dc2-9532-8d3fc6a9a4ed"),
                    Title = "The Incredibles",
                    Description = "A family of undercover superheroes, while trying to live the quiet suburban life, are forced into action to save the world.",
                    Director = "Brad Bird",
                    Cast = "Craig T. Nelson, Holly Hunter, Samuel L. Jackson",
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTY5OTU0OTc2NV5BMl5BanBnXkFtZTcwMzU4MDcyMQ@@._V1_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=eZbzbC9285I",
                    Duration = 115,
                    GenreId = Guid.Parse("ff41706e-522a-47cf-93e3-e646823ea7ed"), 
                    Rating = 4,
                    ReleaseDate = new DateTime(2004, 11, 05),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                },
                new Movie()
                {
                    Id = Guid.Parse("89d143c2-41f5-4e49-b9b8-3152df7c2f8e"),
                    Title = "The Lord of the Rings: The Return of the King",
                    Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                    Director = "Peter Jackson",
                    Cast = "Elijah Wood, Viggo Mortensen, Ian McKellen, Orlando Bloom, Cate Blanchett",
                    PosterUrl = "https://m.media-amazon.com/images/I/51Qvs9i5a%2BL._AC_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=r5X-hFf6Bwo",
                    Duration = 201,
                    GenreId = Guid.Parse("6f7c25cb-88f7-4036-902e-473b8b7f06d0"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2003, 12, 17),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
                new Movie()
                {
                    Id = Guid.Parse("4e2c34e7-5b85-4a72-a894-8e71bb045c6e"),
                    Title = "A Quiet Place",
                    Description = "In a post-apocalyptic world, a family is forced to live in silence while hiding from monsters with ultra-sensitive hearing.",
                    Director = "John Krasinski",
                    Cast = "Emily Blunt, John Krasinski, Millicent Simmonds, Noah Jupe",
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMjI0MDMzNTQ0M15BMl5BanBnXkFtZTgwMTM5NzM3NDM@._V1_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=WR7cc5t7tv8",
                    Duration = 90,
                    GenreId = Guid.Parse("a8dbee99-43f7-4df1-824b-4b27600f67bd"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2018, 04, 06),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                }
            };
            return movies;
        }
    }
}
