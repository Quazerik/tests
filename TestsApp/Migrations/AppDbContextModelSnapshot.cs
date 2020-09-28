﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestsApp;

namespace TestsApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("TestsApp.Models.Test.Answers.Answer", b =>
            {
                b.Property<int>("TestResultId");

                b.Property<int>("QuestionNumber");

                b.Property<string>("Discriminator")
                    .IsRequired();

                b.HasKey("TestResultId", "QuestionNumber");

                b.ToTable("Answers");

                b.HasDiscriminator<string>("Discriminator").HasValue("Answer");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Questions.Question", b =>
            {
                b.Property<int>("TestId");

                b.Property<int>("Number");

                b.Property<string>("Discriminator")
                    .IsRequired();

                b.Property<int>("Score");

                b.Property<string>("Text");

                b.HasKey("TestId", "Number");

                b.ToTable("Questions");

                b.HasDiscriminator<string>("Discriminator").HasValue("Question");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Test", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("GroupId");

                b.Property<string>("Name");

                b.Property<int>("TimeInSeconds");

                b.HasKey("Id");

                b.HasIndex("GroupId");

                b.ToTable("Tests");
            });

            modelBuilder.Entity("TestsApp.Models.Test.TestResult", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime?>("FinishTime");

                b.Property<int?>("Result");

                b.Property<DateTime>("StartTime");

                b.Property<string>("StudentId");

                b.Property<int>("TestId");

                b.HasKey("Id");

                b.HasIndex("StudentId");

                b.HasIndex("TestId");

                b.ToTable("TestResults");
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

            modelBuilder.Entity("TestsApp.Models.Test.Answers.MultiChoiceAnswer", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Answers.Answer");

                b.Property<string>("SerializedAnswersNums");

                b.ToTable("MultiChoiceAnswer");

                b.HasDiscriminator().HasValue("MultiChoiceAnswer");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Answers.OneChoiceAnswer", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Answers.Answer");

                b.Property<int>("ChosenAnswerNum");

                b.ToTable("OneChoiceAnswer");

                b.HasDiscriminator().HasValue("OneChoiceAnswer");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Answers.OpenAnswerAnswer", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Answers.Answer");

                b.Property<string>("Answer");

                b.ToTable("OpenAnswerAnswer");

                b.HasDiscriminator().HasValue("OpenAnswerAnswer");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Answers.TwoColumnsAnswer", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Answers.Answer");

                b.Property<string>("SerializedConnections");

                b.ToTable("TwoColumnsAnswer");

                b.HasDiscriminator().HasValue("TwoColumnsAnswer");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Questions.OpenAnswerQuestion", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Questions.Question");

                b.Property<string>("RightAnswer");

                b.ToTable("OpenAnswerQuestion");

                b.HasDiscriminator().HasValue("OpenAnswerQuestion");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Questions.QuestionWithAnswerVariants", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Questions.Question");

                b.Property<string>("SerializedAnswerVariants");

                b.ToTable("QuestionWithAnswerVariants");

                b.HasDiscriminator().HasValue("QuestionWithAnswerVariants");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Questions.TwoColumnsQuestion", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Questions.Question");

                b.Property<string>("SerializedConnections");

                b.Property<string>("SerializedLeft");

                b.Property<string>("SerializedRight");

                b.ToTable("TwoColumnsQuestion");

                b.HasDiscriminator().HasValue("TwoColumnsQuestion");
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

            modelBuilder.Entity("TestsApp.Models.Test.Questions.MultiChoiceQuestion", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Questions.QuestionWithAnswerVariants");

                b.Property<string>("SerializedRightAnswersNums");

                b.ToTable("MultiChoiceQuestion");

                b.HasDiscriminator().HasValue("MultiChoiceQuestion");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Questions.OneChoiceQuestion", b =>
            {
                b.HasBaseType("TestsApp.Models.Test.Questions.QuestionWithAnswerVariants");

                b.Property<int>("RightAnswerNum");

                b.ToTable("OneChoiceQuestion");

                b.HasDiscriminator().HasValue("OneChoiceQuestion");
            });

            modelBuilder.Entity("TestsApp.Models.Group", b =>
            {
                b.HasOne("TestsApp.Models.Users.TeacherUser", "Teacher")
                    .WithMany("Groups")
                    .HasForeignKey("TeacherId");
            });

            modelBuilder.Entity("TestsApp.Models.Test.Answers.Answer", b =>
            {
                b.HasOne("TestsApp.Models.Test.TestResult", "TestResult")
                    .WithMany("Answers")
                    .HasForeignKey("TestResultId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("TestsApp.Models.Test.Questions.Question", b =>
            {
                b.HasOne("TestsApp.Models.Test.Test", "Test")
                    .WithMany("Questions")
                    .HasForeignKey("TestId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("TestsApp.Models.Test.Test", b =>
            {
                b.HasOne("TestsApp.Models.Group", "Group")
                    .WithMany()
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("TestsApp.Models.Test.TestResult", b =>
            {
                b.HasOne("TestsApp.Models.Users.StudentUser", "Student")
                    .WithMany()
                    .HasForeignKey("StudentId");

                b.HasOne("TestsApp.Models.Test.Test", "Test")
                    .WithMany()
                    .HasForeignKey("TestId")
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
