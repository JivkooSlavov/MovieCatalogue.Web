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

            builder.Property(m => m.Director)
                .IsRequired()
                .HasMaxLength(MovieDirectorMaxLength);

            builder
                .Property(m => m.IsDeleted)
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
                    Description = "The story follows 11-year-old Harry Potter, who discovers on his birthday that he is a wizard. Raised by his cruel aunt and uncle, Harry learns that he is famous in the magical world for surviving an attack by the dark wizard Lord Voldemort, which left him with a lightning-shaped scar on his forehead. Harry is invited to attend Hogwarts School of Witchcraft and Wizardry, where he befriends Ron and Hermione. As they explore the mysteries of the school, they uncover a secret about the Philosopher's Stone, a magical object that grants immortality.",
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
                    Description = "TIn the peaceful Shire, Bilbo Baggins is visited by the wizard Gandalf (Ian McKellen) and a group of 13 dwarves, led by Thorin Oakenshield (Richard Armitage), who seek his help to reclaim the Lonely Mountain and its treasure, which has been overtaken by the fearsome dragon Smaug. Though Bilbo is initially reluctant to join their quest, he eventually agrees, setting off on an unexpected adventure that will take him across the dangerous and magical lands of Middle-earth.",
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
                    Description = "The story centers around the mysterious disappearance of Amy Dunne on the morning of her fifth wedding anniversary. As Nick becomes the prime suspect in her case, the investigation reveals troubling details about the couple's relationship. The film alternates between Nick's perspective and flashbacks through Amy's diary entries, gradually revealing a complex and twisted story of manipulation, deception, and psychological games.",
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
                    Description = "The plot centers around the invasion of the city of Troy by the Greeks. The war is sparked when Paris, the prince of Troy, abducts Helen, the wife of Menelaus, the king of Sparta. In retaliation, Menelaus and his brother Agamemnon, the king of Mycenae, lead a Greek alliance to lay siege to Troy. The Greeks assemble an army that includes legendary warriors such as Achilles, the greatest of them all, and his rival Hector, the noble Trojan prince.",
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
                    Description = "A gruff and solitary mammoth named Manny (voiced by Ray Romano) embarks on an unexpected journey when he crosses paths with Sid (voiced by John Leguizamo), a talkative sloth, and Diego (voiced by Denis Leary), a cunning saber-toothed tiger. Despite their differences, the trio is forced to work together when they discover a human infant. Manny, moved by a sense of duty, takes it upon himself to protect the baby and return it to its tribe. Along the way, the unlikely group faces numerous challenges, including predators hunting them, harsh environmental conditions, and natural obstacles, all while forging a bond that transcends their initial differences.",
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
                    Id = Guid.Parse("24e4cfc0-ec76-4f80-9d4c-e2f5bba1052e"),
                    Title = "Avatar",
                    Description = "The story follows Jake Sully (played by Sam Worthington), a paraplegic former Marine who is recruited to replace his deceased twin brother as an operator of an \"avatar\"—a remotely controlled, genetically engineered body that allows humans to interact with the indigenous Na'vi people on Pandora. The Na'vi are a humanoid species with blue skin and a deep connection to their environment. The human presence on Pandora is driven by the desire to mine a valuable mineral called unobtanium, which is found deep within the planet's forests.",
                    Director = "James Cameron",
                    Cast = "Sam Worthington, Zoe Saldana, Sigourney Weaver, Stephen Lang, Michelle Rodriguez, Giovanni Ribisi",
                    PosterUrl = "https://m.media-amazon.com/images/I/41kTVLeW1CL._AC_UF894,1000_QL80_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=5PSNL1qE6VY",
                    Duration = 162,
                    GenreId = Guid.Parse("93df566f-30f5-4d2b-b6ff-dc540b86a289"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2009, 12, 18),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
                new Movie()
                {
                    Id = Guid.Parse("d4e6bf33-5a79-490a-89d1-dc9e2fe12b5c"),
                    Title = "Forrest Gump",
                    Description = "Forrest Gump tells the story of a simple man with a good heart who unintentionally influences major historical events and changes the lives of everyone he meets. From his humble beginnings in Alabama to becoming a war hero, a successful businessman, and a cultural icon, Forrest's journey is a testament to perseverance, love, and the power of kindness. At its core, the film explores themes of destiny, resilience, and the unpredictability of life.",
                    Director = "Robert Zemeckis",
                    Cast = "Tom Hanks, Robin Wright, Gary Sinise, Mykelti Williamson, Sally Field",
                    PosterUrl = "https://static0.srcdn.com/wordpress/wp-content/uploads/2023/05/forrest-gump-movie-poster.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=bLvqoHBptjg",
                    Duration = 142,
                    GenreId = Guid.Parse("c2e80bd4-44f7-42f2-8a76-189fe9d12754"),
                    Rating = 5,
                    ReleaseDate = new DateTime(1994, 07, 06),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
                new Movie()
                {
                    Id = Guid.Parse("47b1d1ca-756b-4f56-93ad-9d07c44d7b7a"),
                    Title = "The Lion King",
                    Description = "Simba, a young lion prince, must overcome adversity and reclaim his rightful place as king after the tragic death of his father, Mufasa. Along his journey, Simba learns about responsibility, love, and what it means to be a leader, all while confronting his wicked uncle, Scar, who has usurped the throne.",
                    Director = "Roger Allers, Rob Minkoff",
                    Cast = "Matthew Broderick, James Earl Jones, Jeremy Irons, Nathan Lane, Rowan Atkinson, Whoopi Goldberg",
                    PosterUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRWwKdahmkJpbNfahOGIhiWjHl1UBLKCHOrqO3BLdR-zxHtGMXf1tjX28xeN30fBl-XXz3-AQ",
                    TrailerUrl = "https://www.youtube.com/watch?v=7TavVZMewpY",
                    Duration = 88,
                    GenreId = Guid.Parse("ff41706e-522a-47cf-93e3-e646823ea7ed"),
                    Rating = 4,
                    ReleaseDate = new DateTime(1994, 06, 24),
                    CreatedByUserId = Guid.Parse("ba09344d-675b-431b-9808-1b92c92ce016")
                },
                new Movie()
                {
                    Id = Guid.Parse("31b7d9c1-b9b1-4a3e-9f71-d3d0fdbd94d2"),
                    Title = "Inception",
                    Description = "Dom Cobb, a thief with the ability to enter people's dreams and steal secrets, is offered a chance to have his criminal record erased if he can perform an inception—implanting an idea in someone's mind without their knowledge. Cobb assembles a team and dives deep into multiple layers of dreams, where time bends and the stakes are life or death.",
                    Director = "Christopher Nolan",
                    Cast = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Tom Hardy, Cillian Murphy",
                    PosterUrl = "https://cdn11.bigcommerce.com/s-yzgoj/images/stencil/1280x1280/products/2919271/5944675/MOVEB46211__19379.1679590452.jpg?c=2",
                    TrailerUrl = "https://www.youtube.com/watch?v=YoHD9XEInc0",
                    Duration = 148,
                    GenreId = Guid.Parse("d4a34098-333d-4177-9cbc-53a979f0382c"),
                    Rating = 4,
                    ReleaseDate = new DateTime(2010, 07, 16),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                },
                    new Movie()
                {
                    Id = Guid.Parse("7a59a1d2-d249-442a-8fbb-b3b4057e2e9d"),
                    Title = "The Dark Knight",
                    Description = "Batman faces his greatest challenge when a criminal mastermind known as the Joker emerges from the shadows. As the Joker unleashes chaos on Gotham City, Batman must confront his own morals and the consequences of his actions, all while facing the growing tension between him and his trusted ally, Gotham City District Attorney Harvey Dent. With the fate of Gotham hanging in the balance, Batman must decide whether to uphold his code or take drastic action to save his city.",
                    Director = "Christopher Nolan",
                    Cast = "Christian Bale, Heath Ledger, Aaron Eckhart, Michael Caine, Gary Oldman, Maggie Gyllenhaal",
                    PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg",
                    TrailerUrl = "https://www.youtube.com/watch?v=EXeTwQWrcwY",
                    Duration = 152,
                    GenreId = Guid.Parse("d4a34098-333d-4177-9cbc-53a979f0382c"),
                    Rating = 5,
                    ReleaseDate = new DateTime(2008, 07, 18),
                    CreatedByUserId = Guid.Parse("2a82b11c-525b-44a4-9d03-a108c6bed3b9")
                },
            };
            return movies;
        }
    }
}