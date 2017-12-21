using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DataAccessLayer;
using Common.Enum;

namespace DataAccessLayer.Migrations
{
    [DbContext(typeof(MSContext))]
    partial class MSContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Common.Entity.AnswerEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<bool>("TrueOrFalse");

                    b.HasKey("Id");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Common.Entity.AnswerForQuestionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AnswerId");

                    b.Property<int?>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("AnswerId");

                    b.HasIndex("QuestionId");

                    b.ToTable("AnswersForQuestion");
                });

            modelBuilder.Entity("Common.Entity.GameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("TotaleScore");

                    b.Property<string>("WinnerPlayer");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Common.Entity.PlayerForGameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameId");

                    b.Property<int>("GameRole");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("PlayerForGameEntity");
                });

            modelBuilder.Entity("Common.Entity.QuestionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Common.Entity.QuestionForTestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("QuestionId");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.HasIndex("TestId");

                    b.ToTable("QuestionsForTest");
                });

            modelBuilder.Entity("Common.Entity.TestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Test");
                });

            modelBuilder.Entity("Common.Entity.TestForGameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GameId");

                    b.Property<int>("TestId");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .IsUnique();

                    b.HasIndex("TestId");

                    b.ToTable("TestsForGame");
                });

            modelBuilder.Entity("Common.Entity.TokenEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ExpirationDateTime");

                    b.Property<string>("UserId");

                    b.Property<string>("UsserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("Common.Entity.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nike");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Common.Entity.AnswerForQuestionEntity", b =>
                {
                    b.HasOne("Common.Entity.AnswerEntity", "Answer")
                        .WithMany()
                        .HasForeignKey("AnswerId");

                    b.HasOne("Common.Entity.QuestionEntity", "Question")
                        .WithMany("AnswerForQuestionEntity")
                        .HasForeignKey("QuestionId");
                });

            modelBuilder.Entity("Common.Entity.PlayerForGameEntity", b =>
                {
                    b.HasOne("Common.Entity.GameEntity", "Game")
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entity.UserEntity", "User")
                        .WithMany("UserPlayerEntity")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Common.Entity.QuestionForTestEntity", b =>
                {
                    b.HasOne("Common.Entity.QuestionEntity", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entity.TestEntity", "Test")
                        .WithMany("QuestionForTestEntity")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entity.TestForGameEntity", b =>
                {
                    b.HasOne("Common.Entity.GameEntity", "Game")
                        .WithOne("Test")
                        .HasForeignKey("Common.Entity.TestForGameEntity", "GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Common.Entity.TestEntity", "Test")
                        .WithMany()
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Common.Entity.TokenEntity", b =>
                {
                    b.HasOne("Common.Entity.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
