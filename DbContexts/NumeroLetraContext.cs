using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NumeroLetraAPI.Models;

namespace NumeroLetraAPI.DbContexts;

public partial class NumeroLetraContext : DbContext
{
    public NumeroLetraContext()
    {
    }

    public NumeroLetraContext(DbContextOptions<NumeroLetraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Log> TblLogs { get; set; }

    public virtual DbSet<User> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.IdLog);

            entity.ToTable("tblLog");

            entity.Property(e => e.IntNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("intNumber");
            entity.Property(e => e.StrNumberLetter)
                .IsUnicode(false)
                .HasColumnName("strNumberLetter");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.ToTable("tblUsers");

            entity.Property(e => e.BitActive).HasColumnName("bitActive");
            entity.Property(e => e.FxCompleteName)
                .HasMaxLength(101)
                .IsUnicode(false)
                .HasComputedColumnSql("(([strName]+' ')+[strLastName])", false)
                .HasColumnName("fxCompleteName");
            entity.Property(e => e.IdUser).ValueGeneratedOnAdd();
            entity.Property(e => e.StrLastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("strLastName");
            entity.Property(e => e.StrName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("strName");
            entity.Property(e => e.StrPassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("strPassword");
            entity.Property(e => e.StrUser)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("strUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
