﻿// <auto-generated />
using System;
using D_APP_Infrastructure.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace D_APP_Infrastructure.Migrations
{
    [DbContext(typeof(DAppContext))]
    partial class DAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("D_APP_Core.Entities.Documents", b =>
                {
                    b.Property<Guid>("DocId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DocCont")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DocCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DocModifiDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DocStatus")
                        .HasColumnType("int");

                    b.Property<string>("DocTitle")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("linkImg")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DocId");

                    b.HasIndex("UserId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("D_APP_Core.Entities.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UserCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UserDateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserFname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserLname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UserModifiDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UserPassword")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("UserPhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<byte?>("UserRole")
                        .HasColumnType("tinyint");

                    b.Property<byte?>("UserStatus")
                        .HasColumnType("tinyint");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("D_APP_Core.Entities.Documents", b =>
                {
                    b.HasOne("D_APP_Core.Entities.User", "User")
                        .WithMany("Documents")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("D_APP_Core.Entities.User", b =>
                {
                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
