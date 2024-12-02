﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieCatalogue.Data;

#nullable disable

namespace MovieCatalogue.Data.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20241201230505_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Favorite", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c2e80bd4-44f7-42f2-8a76-189fe9d12754"),
                            Name = "Drama"
                        },
                        new
                        {
                            Id = new Guid("5a5e5959-17ee-4b2a-8902-497a1c79321e"),
                            Name = "Comedy"
                        },
                        new
                        {
                            Id = new Guid("d4a34098-333d-4177-9cbc-53a979f0382c"),
                            Name = "Action"
                        },
                        new
                        {
                            Id = new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"),
                            Name = "Adventure"
                        },
                        new
                        {
                            Id = new Guid("a8dbee99-43f7-4df1-824b-4b27600f67bd"),
                            Name = "Horror"
                        },
                        new
                        {
                            Id = new Guid("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"),
                            Name = "Triller"
                        },
                        new
                        {
                            Id = new Guid("e692d3ba-17f1-466d-b8e5-aac14c4d8242"),
                            Name = "Mystery"
                        },
                        new
                        {
                            Id = new Guid("d5ce01d4-9ac0-411c-b813-e022d6886652"),
                            Name = "History"
                        },
                        new
                        {
                            Id = new Guid("ff41706e-522a-47cf-93e3-e646823ea7ed"),
                            Name = "Animation"
                        },
                        new
                        {
                            Id = new Guid("93df566f-30f5-4d2b-b6ff-dc540b86a289"),
                            Name = "Fantasy"
                        });
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Movie", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cast")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<Guid>("CreatedByUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<double>("Rating")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(2, 1)
                        .HasColumnType("float(2)")
                        .HasDefaultValue(0.0);

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("TrailerUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("GenreId");

                    b.ToTable("Movies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9a237ae5-bff5-4fa3-b30f-1303ea504903"),
                            Cast = "Daniel Radcliffe, Rupert Grint, Emma Watson, John Cleese, Robbie Coltrane, Warwick Davis, Richard Griffiths, Richard Harris, Ian Hart, John Hurt, Alan Rickman, Fiona Shaw, Maggie Smith, Julie Walters",
                            CreatedByUserId = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            Description = "Harry Potter and the Philosopher's Stone (also known as Harry Potter and the Sorcerer's Stone in the United States) is a 2001 fantasy film directed by Chris Columbus and produced by David Heyman from a screenplay by Steve Kloves. It is based on the 1997 novel Harry Potter and the Philosopher's Stone by J. K. Rowling. It is the first instalment in the Harry Potter film series. The film stars Daniel Radcliffe as Harry Potter, with Rupert Grint as Ron Weasley, and Emma Watson as Hermione Granger. Its story follows Harry's first year at Hogwarts School of Witchcraft and Wizardry as he discovers that he is a famous wizard and begins his formal wizarding education.",
                            Director = "Chris Columbus",
                            Duration = 152,
                            GenreId = new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"),
                            IsDeleted = false,
                            PosterUrl = "https://img.posterstore.com/zoom/wb0101-8harrypotter-thephilosophersstoneno150x70.jpg",
                            Rating = 5.0,
                            ReleaseDate = new DateTime(2001, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Harry Potter and the Philosopher's Stone",
                            TrailerUrl = "https://www.youtube.com/watch?v=VyHV0BRtdxo"
                        },
                        new
                        {
                            Id = new Guid("b1d07192-c121-443a-8367-b18934739a2e"),
                            Cast = "Ian McKellen, Martin Freeman, Richard Armitage, James Nesbitt, Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, Elijah Wood, Andy Serkis",
                            CreatedByUserId = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            Description = "The story is set in Middle-earth sixty years before the main events of The Lord of the Rings and portions of the film are adapted from the appendices to Tolkien's The Return of the King.[7] An Unexpected Journey tells the tale of Bilbo Baggins (Martin Freeman), who is convinced by the wizard Gandalf (Ian McKellen) to accompany thirteen Dwarves, led by Thorin Oakenshield (Richard Armitage), on a quest to reclaim the Lonely Mountain from the dragon Smaug. The ensemble cast also includes Ken Stott, Cate Blanchett, Ian Holm, Christopher Lee, Hugo Weaving, James Nesbitt, Elijah Wood, and Andy Serkis. It features Sylvester McCoy, Barry Humphries, and Manu Bennett.",
                            Director = "Peter Jackson",
                            Duration = 169,
                            GenreId = new Guid("6f7c25cb-88f7-4036-902e-473b8b7f06d0"),
                            IsDeleted = false,
                            PosterUrl = "https://m.media-amazon.com/images/M/MV5BMTcwNTE4MTUxMl5BMl5BanBnXkFtZTcwMDIyODM4OA@@._V1_FMjpg_UX1000_.jpg",
                            Rating = 5.0,
                            ReleaseDate = new DateTime(2012, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Hobbit: An Unexpected Journey",
                            TrailerUrl = "https://www.youtube.com/watch?v=9PSXjr1gbjc"
                        },
                        new
                        {
                            Id = new Guid("000a7d61-c851-4705-9e18-f16ea9624cf7"),
                            Cast = "Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry",
                            CreatedByUserId = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            Description = "Gone Girl is aAmerican psychological thriller film directed by David Fincher and written by Gillian Flynn, based on her 2012 novel of the same name. It stars Ben Affleck, Rosamund Pike, Neil Patrick Harris, Tyler Perry, and Carrie Coon in her film debut. In the film, Nick Dunne (Affleck) becomes the prime suspect in the sudden disappearance of his wife, Amy (Pike) in Missouri.",
                            Director = "David Fincher",
                            Duration = 149,
                            GenreId = new Guid("947fa7e5-5013-4c8e-b03a-8e2aa6317cd8"),
                            IsDeleted = false,
                            PosterUrl = "https://m.media-amazon.com/images/I/61cdYGoHFrL._AC_SY879_.jpg",
                            Rating = 5.0,
                            ReleaseDate = new DateTime(2014, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Gone Girl",
                            TrailerUrl = "https://www.youtube.com/watch?v=2-_-1nJf8Vg"
                        },
                        new
                        {
                            Id = new Guid("7dc6c01d-68ed-475c-9ca1-3cda99437db2"),
                            Cast = "Brad Pitt, Eric Bana, Orlando Bloom, Diane Kruger, Brian Cox, Sean Bean, Brendan Gleeson, Peter O'Toole",
                            CreatedByUserId = new Guid("ba09344d-675b-431b-9808-1b92c92ce016"),
                            Description = "Troy is a 2004 epic historical war film directed by Wolfgang Petersen and written by David Benioff. Produced by units in Malta, Mexico and Britain's Shepperton Studios, the film features an ensemble cast led by Brad Pitt, Eric Bana, Sean Bean, Diane Kruger, Brian Cox, Brendan Gleeson, Rose Byrne, Saffron Burrows and Orlando Bloom. It is loosely based[3] on Homer's Iliad in its narration of the entire story of the decade-long Trojan War—condensed into little more than a couple of weeks, rather than just the quarrel between Achilles and Agamemnon in the ninth year. Achilles leads his Myrmidons along with the rest of the Greek army invading the historical city of Troy, defended by Hector's Trojan army. The end of the film (the sack of Troy) is not taken from the Iliad, but rather from Quintus Smyrnaeus's Posthomerica, as the Iliad concludes with Hector's death and funeral.",
                            Director = "Wolfgang Petersen",
                            Duration = 183,
                            GenreId = new Guid("d5ce01d4-9ac0-411c-b813-e022d6886652"),
                            IsDeleted = false,
                            PosterUrl = "https://www.rogerebert.com/wp-content/uploads/2024/03/Troy.jpg",
                            Rating = 5.0,
                            ReleaseDate = new DateTime(2004, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Troy",
                            TrailerUrl = "https://www.youtube.com/watch?v=znTLzRJimeY"
                        },
                        new
                        {
                            Id = new Guid("e8772d7c-8bda-4652-9f91-757857838db7"),
                            Cast = "Ray Romano, John Leguizamo, Denis Leary",
                            CreatedByUserId = new Guid("ba09344d-675b-431b-9808-1b92c92ce016"),
                            Description = "On Earth 20,000 years ago, everything was covered in ice. A group of friends, Manny, a mammoth, Diego, a saber tooth tiger, and Sid, a sloth encounter an Eskimo human baby. They must try to return the baby back to his tribe before a group of saber tooth tigers find him and eat him.",
                            Director = "Chris Wedge",
                            Duration = 81,
                            GenreId = new Guid("ff41706e-522a-47cf-93e3-e646823ea7ed"),
                            IsDeleted = false,
                            PosterUrl = "https://image.tmdb.org/t/p/original/idg7vYulQRXKEHfLIM0XKtqPkPz.jpg",
                            Rating = 4.0,
                            ReleaseDate = new DateTime(2003, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Ice Age",
                            TrailerUrl = "https://www.youtube.com/watch?v=wjdqn9r4thg"
                        });
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(5)
                        .HasPrecision(2, 1)
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Ratings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d13f3150-3827-45db-8d78-07f8b69288c0"),
                            MovieId = new Guid("9a237ae5-bff5-4fa3-b30f-1303ea504903"),
                            UserId = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            Value = 5
                        },
                        new
                        {
                            Id = new Guid("418b2462-0485-458b-9d67-59a213724a29"),
                            MovieId = new Guid("b1d07192-c121-443a-8367-b18934739a2e"),
                            UserId = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            Value = 5
                        },
                        new
                        {
                            Id = new Guid("517b4432-a16e-4b71-abe1-87c9225b29e7"),
                            MovieId = new Guid("000a7d61-c851-4705-9e18-f16ea9624cf7"),
                            UserId = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            Value = 5
                        },
                        new
                        {
                            Id = new Guid("2ab20663-9c23-4be5-8177-a27c04e36523"),
                            MovieId = new Guid("7dc6c01d-68ed-475c-9ca1-3cda99437db2"),
                            UserId = new Guid("ba09344d-675b-431b-9808-1b92c92ce016"),
                            Value = 5
                        },
                        new
                        {
                            Id = new Guid("4dd82023-5ea3-4b17-837c-275f9a508a60"),
                            MovieId = new Guid("e8772d7c-8bda-4652-9f91-757857838db7"),
                            UserId = new Guid("ba09344d-675b-431b-9808-1b92c92ce016"),
                            Value = 4
                        });
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<DateTime>("DatePosted")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatePosted")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("2a82b11c-525b-44a4-9d03-a108c6bed3b9"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "efd90642-525d-43f4-9740-63fde54eed5c",
                            Email = "zhivko@movie.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "ZHIVKO@MOVIE.COM",
                            NormalizedUserName = "ZHIVKO@MOVIE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEGcznz/TVQIi2IH842oICmpQNdeNGWYEkDQiT444BIUF5LqiPmpaWrZEM3zsP1jWQw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "f0c6045d-ea27-4f92-aaf6-2edc794180ab",
                            TwoFactorEnabled = false,
                            UserName = "zhivko@movie.com"
                        },
                        new
                        {
                            Id = new Guid("ba09344d-675b-431b-9808-1b92c92ce016"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d59ba3e5-8622-43d6-b218-8692c8245a79",
                            Email = "mitko@movie.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "MITKO@MOVIE.COM",
                            NormalizedUserName = "MITKO@MOVIE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOt9dip+k2wa/ig57dnpZKnHu1tffiURyM6+p4Es8gbbTarWxwTzMTltaWMKdeGYiA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "33eba3d7-9f5c-40b7-a03d-23a7c44acbc1",
                            TwoFactorEnabled = false,
                            UserName = "mitko@movie.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieCatalogue.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Favorite", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.Movie", "Movie")
                        .WithMany("Favorites")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MovieCatalogue.Data.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Movie", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.User", "CreatedByUser")
                        .WithMany("CreatedMovies")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieCatalogue.Data.Models.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByUser");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Rating", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.Movie", "Movie")
                        .WithMany("Ratings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MovieCatalogue.Data.Models.User", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Review", b =>
                {
                    b.HasOne("MovieCatalogue.Data.Models.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MovieCatalogue.Data.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.Movie", b =>
                {
                    b.Navigation("Favorites");

                    b.Navigation("Ratings");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MovieCatalogue.Data.Models.User", b =>
                {
                    b.Navigation("CreatedMovies");

                    b.Navigation("Favorites");

                    b.Navigation("Ratings");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}