using System;
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
                    { new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 0, "efd90642-525d-43f4-9740-63fde54eed5c", "zhivko@movie.com", false, false, null, "ZHIVKO@MOVIE.COM", "ZHIVKO@MOVIE.COM", "AQAAAAIAAYagAAAAEGcznz/TVQIi2IH842oICmpQNdeNGWYEkDQiT444BIUF5LqiPmpaWrZEM3zsP1jWQw==", null, false, "f0c6045d-ea27-4f92-aaf6-2edc794180ab", false, "zhivko@movie.com" },
                    { new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 0, "d59ba3e5-8622-43d6-b218-8692c8245a79", "mitko@movie.com", false, false, null, "MITKO@MOVIE.COM", "MITKO@MOVIE.COM", "AQAAAAIAAYagAAAAEOt9dip+k2wa/ig57dnpZKnHu1tffiURyM6+p4Es8gbbTarWxwTzMTltaWMKdeGYiA==", null, false, "33eba3d7-9f5c-40b7-a03d-23a7c44acbc1", false, "mitko@movie.com" }
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
                    { new Guid("000a7d61-c851-4705-9e18-f16ea9624cf7"), "Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "Gone Girl is aAmerican psychological thriller film directed by David Fincher and written by Gillian Flynn, based on her 2012 novel of the same name. It stars Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry, and Carrie Coon in her film debut. In the film, Nick Dunne (Affleck) becomes the prime suspect in the sudden disappearance of his wife, Amy (Pike) in Missouri.", "David Fincher", 149, new Guid("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"), "https://m.media-amazon.com/images/I/61cdYGoHFrL._AC_SY879_.jpg", 5.0, new DateTime(2014, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gone Girl", "https://www.youtube.com/watch?v=2-_-1nJf8Vg" },
                    { new Guid("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), "Brad Pitt, Eric Bana, Orlando Bloom, Diane Kruger, Brian Cox, Sean Bean, Brendan Gleeson, Peter O'Toole", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "Troy is a 2004 epic historical war film directed by Wolfgang Petersen and written by David Benioff. Produced by units in Malta, Mexico and Britain's Shepperton Studios, the film features an ensemble cast led by Brad Pitt, Eric Bana, Sean Bean, Diane Kruger, Brian Cox, Brendan Gleeson, Rose Byrne, Saffron Burrows and Orlando Bloom. It is loosely based[3] on Homer's Iliad in its narration of the entire story of the decade-long Trojan War—condensed into little more than a couple of weeks, rather than just the quarrel between Achilles and Agamemnon in the ninth year. Achilles leads his Myrmidons along with the rest of the Greek army invading the historical city of Troy, defended by Hector's Trojan army. The end of the film (the sack of Troy) is not taken from the Iliad, but rather from Quintus Smyrnaeus's Posthomerica, as the Iliad concludes with Hector's death and funeral.", "Wolfgang Petersen", 183, new Guid("d5ce01d4-9ac0-411c-b813-e022d6886652"), "https://www.rogerebert.com/wp-content/uploads/2024/03/Troy.jpg", 5.0, new DateTime(2004, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Troy", "https://www.youtube.com/watch?v=znTLzRJimeY" },
                    { new Guid("9a237ae5-bff5-4fa3-b30f-1303ea504903"), "Daniel Radcliffe, Rupert Grint, Emma Watson, John Cleese, Robbie Coltrane, Warwick Davis, Richard Griffiths, Richard Harris, Ian Hart, John Hurt, Alan Rickman, Fiona Shaw, Maggie Smith, Julie Walters", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "Harry Potter and the Philosopher's Stone (also known as Harry Potter and the Sorcerer's Stone in the United States) is a 2001 fantasy film directed by Chris Columbus and produced by David Heyman from a screenplay by Steve Kloves. It is based on the 1997 novel Harry Potter and the Philosopher's Stone by J. K. Rowling. It is the first instalment in the Harry Potter film series. The film stars Daniel Radcliffe as Harry Potter, with Rupert Grint as Ron Weasley, and Emma Watson as Hermione Granger. Its story follows Harry's first year at Hogwarts School of Witchcraft and Wizardry as he discovers that he is a famous wizard and begins his formal wizarding education.", "Chris Columbus", 152, new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"), "https://img.posterstore.com/zoom/wb0101-8harrypotter-thephilosophersstoneno150x70.jpg", 5.0, new DateTime(2001, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Harry Potter and the Philosopher's Stone", "https://www.youtube.com/watch?v=VyHV0BRtdxo" },
                    { new Guid("b1d07192-c121-443a-8367-b18934739a2e"), "Ian McKellen, Martin Freeman, Richard Armitage, James Nesbitt, Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, Elijah Wood, Andy Serkis", new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), "The story is set in Middle-earth sixty years before the main events of The Lord of the Rings and portions of the film are adapted from the appendices to Tolkien's The Return of the King.[7] An Unexpected Journey tells the tale of Bilbo Baggins (Martin Freeman), who is convinced by the wizard Gandalf (Ian McKellen) to accompany thirteen Dwarves, led by Thorin Oakenshield (Richard Armitage), on a quest to reclaim the Lonely Mountain from the dragon Smaug. The ensemble cast also includes Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, James Nesbitt, Elijah Wood, and Andy Serkis. It features Sylvester McCoy, Barry Humphries, and Manu Bennett.", "Peter Jackson", 169, new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"), "https://m.media-amazon.com/images/M/MV5BMTcwNTE4MTUxMl5BMl5BanBnXkFtZTcwMDIyODM4OA@@._V1_FMjpg_UX1000_.jpg", 5.0, new DateTime(2012, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hobbit: An Unexpected Journey", "https://www.youtube.com/watch?v=9PSXjr1gbjc" },
                    { new Guid("e8772d7c-8bda-4652-9f91-757857838db7"), "Ray Romano, John Leguizamo, Denis Leary", new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), "On Earth 20,000 years ago, everything was covered in ice. A group of friends, Manny, a mammoth, Diego, a saber tooth tiger, and Sid, a sloth encounter an Eskimo human baby. They must try to return the baby back to his tribe before a group of saber tooth tigers find him and eat him.", "Chris Wedge", 81, new Guid("ff41706e-522a-47cf-93e3-e646823ea7ed"), "https://image.tmdb.org/t/p/original/idg7vYulQRXKEHfLIM0XKtqPkPz.jpg", 4.0, new DateTime(2003, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ice Age", "https://www.youtube.com/watch?v=wjdqn9r4thg" }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "MovieId", "UserId", "Value" },
                values: new object[,]
                {
                    { new Guid("2ab20663-9c23-4be5-8177-a27c04e36523"), new Guid("7dc6c01d-68ed-475c-9ca1-3cda99437db2"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 5 },
                    { new Guid("418b2462-0485-458b-9d67-59a213724a29"), new Guid("b1d07192-c121-443a-8367-b18934739a2e"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 },
                    { new Guid("4dd82023-5ea3-4b17-837c-275f9a508a60"), new Guid("e8772d7c-8bda-4652-9f91-757857838db7"), new Guid("ba09344d-675b-431b-9808-1b92c92ce016"), 4 },
                    { new Guid("517b4432-a16e-4b71-abe1-87c9225b29e7"), new Guid("000a7d61-c851-4705-9e18-f16ea9624cf7"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 },
                    { new Guid("d13f3150-3827-45db-8d78-07f8b69288c0"), new Guid("9a237ae5-bff5-4fa3-b30f-1303ea504903"), new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"), 5 }
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
