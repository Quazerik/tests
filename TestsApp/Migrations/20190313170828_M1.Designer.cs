﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestsApp;

namespace TestsApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190313170828_M1")]
    partial class M1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("TestsApp.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("TeacherId");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("TestsApp.Models.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Tests");
                });

            modelBuilder.Entity("TestsApp.Models.Users.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AppUser");
                });

            modelBuilder.Entity("TestsApp.Models.Users.AdminUser", b =>
                {
                    b.HasBaseType("TestsApp.Models.Users.AppUser");

                    b.Property<string>("AdminData");

                    b.ToTable("AdminUser");

                    b.HasDiscriminator().HasValue("AdminUser");
                });

            modelBuilder.Entity("TestsApp.Models.Users.StudentUser", b =>
                {
                    b.HasBaseType("TestsApp.Models.Users.AppUser");

                    b.Property<int>("GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("StudentUser");

                    b.HasDiscriminator().HasValue("StudentUser");
                });

            modelBuilder.Entity("TestsApp.Models.Users.TeacherUser", b =>
                {
                    b.HasBaseType("TestsApp.Models.Users.AppUser");


                    b.ToTable("TeacherUser");

                    b.HasDiscriminator().HasValue("TeacherUser");
                });

            modelBuilder.Entity("TestsApp.Models.Group", b =>
                {
                    b.HasOne("TestsApp.Models.Users.TeacherUser", "Teacher")
                        .WithMany("Groups")
                        .HasForeignKey("TeacherId");
                });

            modelBuilder.Entity("TestsApp.Models.Test", b =>
                {
                    b.HasOne("TestsApp.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestsApp.Models.Users.StudentUser", b =>
                {
                    b.HasOne("TestsApp.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}