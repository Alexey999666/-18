﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Практическая_номер_18.ModelsDB;

public partial class SessionContext : DbContext
{
    public SessionContext()
    {
    }

    public SessionContext(DbContextOptions<SessionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SessionFirstCourseBt> SessionFirstCourseBts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress; Database=Session; User=исп-31; Password= 1234567890; Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SessionFirstCourseBt>(entity =>
        {
            entity.HasKey(e => e.IndexGroup);

            entity.ToTable("SessionFirstCourseBT");

            entity.Property(e => e.FamilyStatus).HasMaxLength(50);
            entity.Property(e => e.Firstname).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Lastname).HasMaxLength(50);
            entity.Property(e => e.Middlename).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
