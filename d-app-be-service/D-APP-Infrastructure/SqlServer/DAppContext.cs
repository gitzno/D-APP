using System;
using System.Collections.Generic;
using D_APP_Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace D_APP_Infrastructure.SqlServer;

public partial class DAppContext : DbContext
{
    public DAppContext(DbContextOptions<DAppContext> options)
              : base(options)
    {
    }

    public virtual DbSet<Documents> Documents { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //        => optionsBuilder.UseSqlServer("server =(local); database = D-APP;uid=sa;pwd=123456;TrustServerCertificate=True");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Documents>()
            .HasOne(d => d.User)
            .WithMany(u => u.Documents)
            .HasForeignKey(d => d.UserId);
    }

}
