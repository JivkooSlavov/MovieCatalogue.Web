﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MovieCatalogue.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cast = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Rating = table.Column<double>(type: "float(2)", precision: 2, scale: 1, nullable: false, defaultValue: 0.0),
                    TrailerUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PosterUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    CreatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movies_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movies_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", maxLength: 5, precision: 2, scale: 1, nullable: false, defaultValue: 1),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatePosted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 0, "32ad60ec-b871-4669-aebb-ba8cbee61931", "zhivko@movie.com", false, false, null, "ZHIVKO@MOVIE.COM", "ZHIVKO@MOVIE.COM", "AQAAAAIAAYagAAAAEKOssnkd71uu940oGcN3TJkqmBDI5cwxuP0TndAoeHlBjP5k7S9eJIfV1Ja72lE9NQ==", null, false, "c7720990-8d49-485d-a5e9-9b5c026db397", false, "zhivko@movie.com" },
                    { new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 0, "f2590093-8aba-42a3-830f-012e87af99ef", "mitko@movie.com", false, false, null, "MITKO@MOVIE.COM", "MITKO@MOVIE.COM", "AQAAAAIAAYagAAAAEC4SrHmfwcr1z1kEo0P8HkWD64FCJyF5setYDwIY5Z0Dne9KFfdyC8wXIokK7fIXvg==", null, false, "bb6fc361-6b8b-4e62-b102-9fa8a32e016a", false, "mitko@movie.com" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5a5e5959-17ee-4b2a-8902-497a1c79321e"), "Comedy" },
                    { new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"), "Adventure" },
                    { new Guid("93df566f-30f5-4d2b-b6ff-dc540b86a289"), "Fantasy" },
                    { new Guid("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"), "Triller" },
                    { new Guid("a8dbee99-43f7-4df1-824b-4b27600f67bd"), "Horror" },
                    { new Guid("c2e80bd4-44f7-42f2-8a76-189fe9d12754"), "Drama" },
                    { new Guid("d4a34098-333d-4177-9cbc-53a979f0382c"), "Action" },
                    { new Guid("d5ce01d4-9ac0-411c-b813-e022d6886652"), "History" },
                    { new Guid("e692d3ba-17f1-466d-b8e5-aac14c4d8242"), "Mystery" },
                    { new Guid("ff41706e-522a-47cf-93e3-e646823ea7ed"), "Animation" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Cast", "CreatedByUserId", "Description", "Director", "Duration", "GenreId", "PosterUrl", "Rating", "ReleaseDate", "Title", "TrailerUrl" },
                values: new object[,]
                {
                    { new Guid("000a7d61-c851-4705-9e18-f16ea9624cf7"), "Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "The story centers around the mysterious disappearance of Amy Dunne on the morning of her fifth wedding anniversary. As Nick becomes the prime suspect in her case, the investigation reveals troubling details about the couple's relationship. The film alternates between Nick's perspective and flashbacks through Amy's diary entries, gradually revealing a complex and twisted story of manipulation, deception, and psychological games.", "David Fincher", 149, new Guid("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"), "https://m.media-amazon.com/images/I/61cdYGoHFrL._AC_SY879_.jpg", 5.0, new DateTime(2014, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gone Girl", "https://www.youtube.com/watch?v=2-_-1nJf8Vg" },
                    { new Guid("24e4cfc0-ec76-4f80-9d4c-e2f5bba1052e"), "Sam Worthington, Zoe Saldana, Sigourney Weaver, Stephen Lang, Michelle Rodriguez, Giovanni Ribisi", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "The story follows Jake Sully (played by Sam Worthington), a paraplegic former Marine who is recruited to replace his deceased twin brother as an operator of an \"avatar\"—a remotely controlled, genetically engineered body that allows humans to interact with the indigenous Na'vi people on Pandora. The Na'vi are a humanoid species with blue skin and a deep connection to their environment. The human presence on Pandora is driven by the desire to mine a valuable mineral called unobtanium, which is found deep within the planet's forests.", "James Cameron", 162, new Guid("93df566f-30f5-4d2b-b6ff-dc540b86a289"), "https://m.media-amazon.com/images/I/41kTVLeW1CL._AC_UF894,1000_QL80_.jpg", 5.0, new DateTime(2009, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Avatar", "https://www.youtube.com/watch?v=5PSNL1qE6VY" },
                    { new Guid("31b7d9c1-b9b1-4a3e-9f71-d3d0fdbd94d2"), "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Tom Hardy, Cillian Murphy", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "Dom Cobb, a thief with the ability to enter people's dreams and steal secrets, is offered a chance to have his criminal record erased if he can perform an inception—implanting an idea in someone's mind without their knowledge. Cobb assembles a team and dives deep into multiple layers of dreams, where time bends and the stakes are life or death.", "Christopher Nolan", 148, new Guid("d4a34098-333d-4177-9cbc-53a979f0382c"), "https://cdn11.bigcommerce.com/s-yzgoj/images/stencil/1280x1280/products/2919271/5944675/MOVEB46211__19379.1679590452.jpg?c=2", 4.0, new DateTime(2010, 7, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Inception", "https://www.youtube.com/watch?v=YoHD9XEInc0" },
                    { new Guid("47b1d1ca-756b-4f56-93ad-9d07c44d7b7a"), "Matthew Broderick, James Earl Jones, Jeremy Irons, Nathan Lane, Rowan Atkinson, Whoopi Goldberg", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "Simba, a young lion prince, must overcome adversity and reclaim his rightful place as king after the tragic death of his father, Mufasa. Along his journey, Simba learns about responsibility, love, and what it means to be a leader, all while confronting his wicked uncle, Scar, who has usurped the throne.", "Roger Allers, Rob Minkoff", 88, new Guid("ff41706e-522a-47cf-93e3-e646823ea7ed"), "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRWwKdahmkJpbNfahOGIhiWjHl1UBLKCHOrqO3BLdR-zxHtGMXf1tjX28xeN30fBl-XXz3-AQ", 4.0, new DateTime(1994, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Lion King", "https://www.youtube.com/watch?v=7TavVZMewpY" },
                    { new Guid("7a59a1d2-d249-442a-8fbb-b3b4057e2e9d"), "Christian Bale, Heath Ledger, Aaron Eckhart, Michael Caine, Gary Oldman, Maggie Gyllenhaal", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "Batman faces his greatest challenge when a criminal mastermind known as the Joker emerges from the shadows. As the Joker unleashes chaos on Gotham City, Batman must confront his own morals and the consequences of his actions, all while facing the growing tension between him and his trusted ally, Gotham City District Attorney Harvey Dent. With the fate of Gotham hanging in the balance, Batman must decide whether to uphold his code or take drastic action to save his city.", "Christopher Nolan", 152, new Guid("d4a34098-333d-4177-9cbc-53a979f0382c"), "https://m.media-amazon.com/images/M/MV5BMTMxNTMwODM0NF5BMl5BanBnXkFtZTcwODAyMTk2Mw@@._V1_.jpg", 5.0, new DateTime(2008, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Dark Knight", "https://www.youtube.com/watch?v=EXeTwQWrcwY" },
                    { new Guid("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), "Brad Pitt, Eric Bana, Orlando Bloom, Diane Kruger, Brian Cox, Sean Bean, Brendan Gleeson, Peter O'Toole", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "The plot centers around the invasion of the city of Troy by the Greeks. The war is sparked when Paris, the prince of Troy, abducts Helen, the wife of Menelaus, the king of Sparta. In retaliation, Menelaus and his brother Agamemnon, the king of Mycenae, lead a Greek alliance to lay siege to Troy. The Greeks assemble an army that includes legendary warriors such as Achilles, the greatest of them all, and his rival Hector, the noble Trojan prince.", "Wolfgang Petersen", 183, new Guid("d5ce01d4-9ac0-411c-b813-e022d6886652"), "https://www.rogerebert.com/wp-content/uploads/2024/03/Troy.jpg", 5.0, new DateTime(2004, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troy", "https://www.youtube.com/watch?v=znTLzRJimeY" },
                    { new Guid("9a237ae5-bff5-4fa3-b30f-1303ea504903"), "Daniel Radcliffe, Rupert Grint, Emma Watson, John Cleese, Robbie Coltrane, Warwick Davis, Richard Griffiths, Richard Harris, Ian Hart, John Hurt, Alan Rickman, Fiona Shaw, Maggie Smith, Julie Walters", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "The story follows 11-year-old Harry Potter, who discovers on his birthday that he is a wizard. Raised by his cruel aunt and uncle, Harry learns that he is famous in the magical world for surviving an attack by the dark wizard Lord Voldemort, which left him with a lightning-shaped scar on his forehead. Harry is invited to attend Hogwarts School of Witchcraft and Wizardry, where he befriends Ron and Hermione. As they explore the mysteries of the school, they uncover a secret about the Philosopher's Stone, a magical object that grants immortality.", "Chris Columbus", 152, new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"), "https://img.posterstore.com/zoom/wb0101-8harrypotter-thephilosophersstoneno150x70.jpg", 5.0, new DateTime(2001, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone", "https://www.youtube.com/watch?v=VyHV0BRtdxo" },
                    { new Guid("b1d07192-c121-443a-8367-b18934739a2e"), "Ian McKellen, Martin Freeman, Richard Armitage, James Nesbitt, Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, Elijah Wood, Andy Serkis", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "TIn the peaceful Shire, Bilbo Baggins is visited by the wizard Gandalf (Ian McKellen) and a group of 13 dwarves, led by Thorin Oakenshield (Richard Armitage), who seek his help to reclaim the Lonely Mountain and its treasure, which has been overtaken by the fearsome dragon Smaug. Though Bilbo is initially reluctant to join their quest, he eventually agrees, setting off on an unexpected adventure that will take him across the dangerous and magical lands of Middle-earth.", "Peter Jackson", 169, new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"), "https://m.media-amazon.com/images/M/MV5BMTcwNTE4MTUxMl5BMl5BanBnXkFtZTcwMDIyODM4OA@@._V1_FMjpg_UX1000_.jpg", 5.0, new DateTime(2012, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hobbit: An Unexpected Journey", "https://www.youtube.com/watch?v=9PSXjr1gbjc" },
                    { new Guid("d4e6bf33-5a79-490a-89d1-dc9e2fe12b5c"), "Tom Hanks, Robin Wright, Gary Sinise, Mykelti Williamson, Sally Field", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "Forrest Gump tells the story of a simple man with a good heart who unintentionally influences major historical events and changes the lives of everyone he meets. From his humble beginnings in Alabama to becoming a war hero, a successful businessman, and a cultural icon, Forrest's journey is a testament to perseverance, love, and the power of kindness. At its core, the film explores themes of destiny, resilience, and the unpredictability of life.", "Robert Zemeckis", 142, new Guid("c2e80bd4-44f7-42f2-8a76-189fe9d12754"), "https://static0.srcdn.com/wordpress/wp-content/uploads/2023/05/forrest-gump-movie-poster.jpg", 5.0, new DateTime(1994, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Forrest Gump", "https://www.youtube.com/watch?v=bLvqoHBptjg" },
                    { new Guid("e8772d7c-8bda-4652-9f91-757857838db7"), "Ray Romano, John Leguizamo, Denis Leary", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "A gruff and solitary mammoth named Manny (voiced by Ray Romano) embarks on an unexpected journey when he crosses paths with Sid (voiced by John Leguizamo), a talkative sloth, and Diego (voiced by Denis Leary), a cunning saber-toothed tiger. Despite their differences, the trio is forced to work together when they discover a human infant. Manny, moved by a sense of duty, takes it upon himself to protect the baby and return it to its tribe. Along the way, the unlikely group faces numerous challenges, including predators hunting them, harsh environmental conditions, and natural obstacles, all while forging a bond that transcends their initial differences.", "Chris Wedge", 81, new Guid("ff41706e-522a-47cf-93e3-e646823ea7ed"), "https://image.tmdb.org/t/p/original/idg7vYulQRXKEHfLIM0XKtqPkPz.jpg", 4.0, new DateTime(2003, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ice Age", "https://www.youtube.com/watch?v=wjdqn9r4thg" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("0106d5e8-7bbd-4e4b-813a-f254a1880a0f"), new Guid("e8772d7c-8bda-4652-9f91-757857838db7"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 4 },
                    { new Guid("133b3f02-859e-4ea4-aefe-8371df14e91e"), new Guid("d4e6bf33-5a79-490a-89d1-dc9e2fe12b5c"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 5 },
                    { new Guid("297c8921-a657-42d4-88aa-46904a1d7960"), new Guid("47b1d1ca-756b-4f56-93ad-9d07c44d7b7a"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 4 },
                    { new Guid("3b26d938-9da4-456e-b611-1150dbec0755"), new Guid("7a59a1d2-d249-442a-8fbb-b3b4057e2e9d"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 },
                    { new Guid("604a2bcd-c977-4330-bcac-1a3ed40b2744"), new Guid("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 5 },
                    { new Guid("6b4cfd80-3ab3-43a1-ad51-0488c328a3a5"), new Guid("24e4cfc0-ec76-4f80-9d4c-e2f5bba1052e"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 5 },
                    { new Guid("6dfe3f57-090a-4022-a725-ef2089261e5b"), new Guid("31b7d9c1-b9b1-4a3e-9f71-d3d0fdbd94d2"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 4 },
                    { new Guid("8945ed4a-e16a-4708-8ee2-265782591dcc"), new Guid("9a237ae5-bff5-4fa3-b30f-1303ea504903"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 },
                    { new Guid("d515793e-b6ae-4939-9caf-413e7565734e"), new Guid("b1d07192-c121-443a-8367-b18934739a2e"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 },
                    { new Guid("f81400d1-b11a-426c-91e7-fea872e8c862"), new Guid("000a7d61-c851-4705-9e18-f16ea9624cf7"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_MovieId",
                table: "Favorites",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatedByUserId",
                table: "Movies",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_GenreId",
                table: "Movies",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_MovieId",
                table: "Ratings",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MovieId",
                table: "Reviews",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}