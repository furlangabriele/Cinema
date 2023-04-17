﻿// <auto-generated />
using System;
using Cinema.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cinema.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("latin1_swedish_ci")
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "latin1");

            modelBuilder.Entity("Cinema.Models.Biglietto", b =>
                {
                    b.Property<int>("FkUtente")
                        .HasColumnType("int(11)")
                        .HasColumnName("fk_utente");

                    b.Property<int>("FkSala")
                        .HasColumnType("int(11)")
                        .HasColumnName("fk_Sala");

                    b.Property<string>("FkFilm")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("fk_Film");

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date")
                        .HasColumnName("data");

                    b.Property<TimeOnly>("Orario")
                        .HasColumnType("time")
                        .HasColumnName("orario");

                    b.Property<int>("Fila")
                        .HasColumnType("int(11)")
                        .HasColumnName("fila");

                    b.Property<int>("Posto")
                        .HasColumnType("int(11)")
                        .HasColumnName("posto");

                    b.HasKey("FkUtente", "FkSala", "FkFilm", "Data", "Orario", "Fila", "Posto")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0, 0, 0, 0 });

                    b.HasIndex(new[] { "FkSala", "FkFilm", "Data", "Orario" }, "fk_Sala");

                    b.ToTable("biglietto", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Film", b =>
                {
                    b.Property<string>("Titolo")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("titolo");

                    b.Property<short?>("AnnoProd")
                        .HasColumnType("year(4)")
                        .HasColumnName("annoProd");

                    b.Property<string>("Descrizione")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("descrizione");

                    b.Property<int>("Durata")
                        .HasColumnType("int(11)")
                        .HasColumnName("durata");

                    b.Property<string>("Genere")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("genere");

                    b.Property<string>("UrlImmPub")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("urlImmPub");

                    b.HasKey("Titolo")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Genere" }, "genere");

                    b.ToTable("film", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Genere", b =>
                {
                    b.Property<string>("Genere1")
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("genere");

                    b.HasKey("Genere1")
                        .HasName("PRIMARY");

                    b.ToTable("genere", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Sala", b =>
                {
                    b.Property<int>("Numero")
                        .HasColumnType("int(11)")
                        .HasColumnName("numero");

                    b.Property<bool>("Isense")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("ISense");

                    b.Property<int>("PostiDisponibili")
                        .HasColumnType("int(11)")
                        .HasColumnName("postiDisponibili");

                    b.HasKey("Numero")
                        .HasName("PRIMARY");

                    b.ToTable("sala", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Spettacolo", b =>
                {
                    b.Property<int>("FkSala")
                        .HasColumnType("int(11)")
                        .HasColumnName("fk_Sala");

                    b.Property<string>("FkFilm")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("fk_Film");

                    b.Property<DateOnly>("Data")
                        .HasColumnType("date")
                        .HasColumnName("data");

                    b.Property<TimeOnly>("Orario")
                        .HasColumnType("time")
                        .HasColumnName("orario");

                    b.HasKey("FkSala", "FkFilm", "Data", "Orario")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                    b.HasIndex(new[] { "FkFilm" }, "fk_Film");

                    b.ToTable("spettacolo", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Utente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(11)")
                        .HasColumnName("id");

                    b.Property<string>("Cognome")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("cognome");

                    b.Property<string>("ComuneRes")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("comuneRes");

                    b.Property<DateOnly>("DataNascita")
                        .HasColumnType("date")
                        .HasColumnName("dataNascita");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("email");

                    b.Property<string>("Nome")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nome");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("password");

                    b.Property<string>("Sesso")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .HasColumnName("sesso")
                        .IsFixedLength();

                    b.HasKey("Id")
                        .HasName("PRIMARY");

                    b.HasIndex(new[] { "Email" }, "email")
                        .IsUnique();

                    b.ToTable("utente", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Valutazione", b =>
                {
                    b.Property<string>("FkFilm")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("fk_Film");

                    b.Property<int>("FkUtente")
                        .HasColumnType("int(11)")
                        .HasColumnName("fk_utente");

                    b.Property<string>("Commento")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("commento");

                    b.Property<int>("Valutazione1")
                        .HasColumnType("int(11)")
                        .HasColumnName("valutazione");

                    b.HasKey("FkFilm", "FkUtente")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "FkUtente" }, "fk_utente");

                    b.ToTable("valutazione", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Cinema.Models.Biglietto", b =>
                {
                    b.HasOne("Cinema.Models.Utente", "FkUtenteNavigation")
                        .WithMany("Bigliettos")
                        .HasForeignKey("FkUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("biglietto_ibfk_2");

                    b.HasOne("Cinema.Models.Spettacolo", "Spettacolo")
                        .WithMany("Bigliettos")
                        .HasForeignKey("FkSala", "FkFilm", "Data", "Orario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("biglietto_ibfk_1");

                    b.Navigation("FkUtenteNavigation");

                    b.Navigation("Spettacolo");
                });

            modelBuilder.Entity("Cinema.Models.Film", b =>
                {
                    b.HasOne("Cinema.Models.Genere", "GenereNavigation")
                        .WithMany("Films")
                        .HasForeignKey("Genere")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("film_ibfk_1");

                    b.Navigation("GenereNavigation");
                });

            modelBuilder.Entity("Cinema.Models.Spettacolo", b =>
                {
                    b.HasOne("Cinema.Models.Film", "FkFilmNavigation")
                        .WithMany("Spettacolos")
                        .HasForeignKey("FkFilm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("spettacolo_ibfk_2");

                    b.HasOne("Cinema.Models.Sala", "FkSalaNavigation")
                        .WithMany("Spettacolos")
                        .HasForeignKey("FkSala")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("spettacolo_ibfk_1");

                    b.Navigation("FkFilmNavigation");

                    b.Navigation("FkSalaNavigation");
                });

            modelBuilder.Entity("Cinema.Models.Valutazione", b =>
                {
                    b.HasOne("Cinema.Models.Film", "FkFilmNavigation")
                        .WithMany("Valutaziones")
                        .HasForeignKey("FkFilm")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("valutazione_ibfk_1");

                    b.HasOne("Cinema.Models.Utente", "FkUtenteNavigation")
                        .WithMany("Valutaziones")
                        .HasForeignKey("FkUtente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("valutazione_ibfk_2");

                    b.Navigation("FkFilmNavigation");

                    b.Navigation("FkUtenteNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cinema.Models.Film", b =>
                {
                    b.Navigation("Spettacolos");

                    b.Navigation("Valutaziones");
                });

            modelBuilder.Entity("Cinema.Models.Genere", b =>
                {
                    b.Navigation("Films");
                });

            modelBuilder.Entity("Cinema.Models.Sala", b =>
                {
                    b.Navigation("Spettacolos");
                });

            modelBuilder.Entity("Cinema.Models.Spettacolo", b =>
                {
                    b.Navigation("Bigliettos");
                });

            modelBuilder.Entity("Cinema.Models.Utente", b =>
                {
                    b.Navigation("Bigliettos");

                    b.Navigation("Valutaziones");
                });
#pragma warning restore 612, 618
        }
    }
}
