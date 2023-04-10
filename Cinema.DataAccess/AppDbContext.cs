using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;

namespace Cinema.DataAccess;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Biglietto> Bigliettos { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Genere> Generes { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Spettacolo> Spettacolos { get; set; }

    public virtual DbSet<Utente> Utentes { get; set; }

    public virtual DbSet<Valutazione> Valutaziones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("latin1_swedish_ci")
            .HasCharSet("latin1");

        modelBuilder.Entity<Biglietto>(entity =>
        {
            entity.HasKey(e => new { e.FkUtente, e.FkSala, e.FkFilm, e.Data, e.Orario, e.Fila, e.Posto })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0, 0, 0, 0 });

            entity.ToTable("biglietto");

            entity.HasIndex(e => new { e.FkSala, e.FkFilm, e.Data, e.Orario }, "fk_Sala");

            entity.Property(e => e.FkUtente)
                .HasColumnType("int(11)")
                .HasColumnName("fk_utente");
            entity.Property(e => e.FkSala)
                .HasColumnType("int(11)")
                .HasColumnName("fk_Sala");
            entity.Property(e => e.FkFilm)
                .HasMaxLength(20)
                .HasColumnName("fk_Film");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Orario)
                .HasColumnType("time")
                .HasColumnName("orario");
            entity.Property(e => e.Fila)
                .HasColumnType("int(11)")
                .HasColumnName("fila");
            entity.Property(e => e.Posto)
                .HasColumnType("int(11)")
                .HasColumnName("posto");

            entity.HasOne(d => d.FkUtenteNavigation).WithMany(p => p.Bigliettos)
                .HasForeignKey(d => d.FkUtente)
                .HasConstraintName("biglietto_ibfk_2");

            entity.HasOne(d => d.Spettacolo).WithMany(p => p.Bigliettos)
                .HasForeignKey(d => new { d.FkSala, d.FkFilm, d.Data, d.Orario })
                .HasConstraintName("biglietto_ibfk_1");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Titolo).HasName("PRIMARY");

            entity.ToTable("film");

            entity.HasIndex(e => e.Genere, "genere");

            entity.Property(e => e.Titolo)
                .HasMaxLength(20)
                .HasColumnName("titolo");
            entity.Property(e => e.AnnoProd)
                .HasColumnType("year(4)")
                .HasColumnName("annoProd");
            entity.Property(e => e.Descrizione)
                .HasMaxLength(500)
                .HasColumnName("descrizione");
            entity.Property(e => e.Durata)
                .HasColumnType("int(11)")
                .HasColumnName("durata");
            entity.Property(e => e.Genere)
                .HasMaxLength(15)
                .HasColumnName("genere");
            entity.Property(e => e.UrlImmPub)
                .HasMaxLength(100)
                .HasColumnName("urlImmPub");

            entity.HasOne(d => d.GenereNavigation).WithMany(p => p.Films)
                .HasForeignKey(d => d.Genere)
                .HasConstraintName("film_ibfk_1");
        });

        modelBuilder.Entity<Genere>(entity =>
        {
            entity.HasKey(e => e.Genere1).HasName("PRIMARY");

            entity.ToTable("genere");

            entity.Property(e => e.Genere1)
                .HasMaxLength(15)
                .HasColumnName("genere");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.Numero).HasName("PRIMARY");

            entity.ToTable("sala");

            entity.Property(e => e.Numero)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("numero");
            entity.Property(e => e.Isense).HasColumnName("ISense");
            entity.Property(e => e.PostiDisponibili)
                .HasColumnType("int(11)")
                .HasColumnName("postiDisponibili");
        });

        modelBuilder.Entity<Spettacolo>(entity =>
        {
            entity.HasKey(e => new { e.FkSala, e.FkFilm, e.Data, e.Orario })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("spettacolo");

            entity.HasIndex(e => e.FkFilm, "fk_Film");

            entity.Property(e => e.FkSala)
                .HasColumnType("int(11)")
                .HasColumnName("fk_Sala");
            entity.Property(e => e.FkFilm)
                .HasMaxLength(20)
                .HasColumnName("fk_Film");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Orario)
                .HasColumnType("time")
                .HasColumnName("orario");

            entity.HasOne(d => d.FkFilmNavigation).WithMany(p => p.Spettacolos)
                .HasForeignKey(d => d.FkFilm)
                .HasConstraintName("spettacolo_ibfk_2");

            entity.HasOne(d => d.FkSalaNavigation).WithMany(p => p.Spettacolos)
                .HasForeignKey(d => d.FkSala)
                .HasConstraintName("spettacolo_ibfk_1");
        });

        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("utente");

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Cognome)
                .HasMaxLength(20)
                .HasColumnName("cognome");
            entity.Property(e => e.ComuneRes)
                .HasMaxLength(20)
                .HasColumnName("comuneRes");
            entity.Property(e => e.DataNascita).HasColumnName("dataNascita");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(20)
                .HasColumnName("nome");
            entity.Property(e => e.Password)
                .HasMaxLength(10)
                .HasColumnName("password");
            entity.Property(e => e.Sesso)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("sesso");
        });

        modelBuilder.Entity<Valutazione>(entity =>
        {
            entity.HasKey(e => new { e.FkFilm, e.FkUtente })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("valutazione");

            entity.HasIndex(e => e.FkUtente, "fk_utente");

            entity.Property(e => e.FkFilm)
                .HasMaxLength(20)
                .HasColumnName("fk_Film");
            entity.Property(e => e.FkUtente)
                .HasColumnType("int(11)")
                .HasColumnName("fk_utente");
            entity.Property(e => e.Commento)
                .HasMaxLength(200)
                .HasColumnName("commento");
            entity.Property(e => e.Valutazione1)
                .HasColumnType("int(11)")
                .HasColumnName("valutazione");

            entity.HasOne(d => d.FkFilmNavigation).WithMany(p => p.Valutaziones)
                .HasForeignKey(d => d.FkFilm)
                .HasConstraintName("valutazione_ibfk_1");

            entity.HasOne(d => d.FkUtenteNavigation).WithMany(p => p.Valutaziones)
                .HasForeignKey(d => d.FkUtente)
                .HasConstraintName("valutazione_ibfk_2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
