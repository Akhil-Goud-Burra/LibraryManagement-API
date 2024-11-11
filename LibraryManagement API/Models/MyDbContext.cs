using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LibraryManagement_API.Models;

public partial class MyDbContext : DbContext
{

    private readonly IConfiguration _configuration;

    public MyDbContext()
    {
    }

    public MyDbContext(IConfiguration configuration, DbContextOptions<MyDbContext> options)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Availability> Availabilities { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Stream> Streams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("LibraryManagement");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Availability>(entity =>
        {
            entity.HasOne(d => d.Book).WithMany(p => p.Availabilities)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Availability_BOOK");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasOne(d => d.Stream).WithMany(p => p.Books)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BOOK_STREAM");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
