using Common.Entity;
using DataAccessLayer.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using StaticDotNet.EntityFrameworkCore.ModelConfiguration;

namespace DataAccessLayer
{
    public class MSContext : DbContext
    {
        public MSContext(DbContextOptions<MSContext> options) : base(options)
        { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<AnswerEntity> Answers { get; set; }
        public DbSet<QuestionEntity> Questions { get; set; }
        public DbSet<PlayerForGameEntity> PlayerForGame { get; set; }
        public DbSet<GameEntity> Games { get; set; }
        public DbSet<TestEntity> Test { get; set; }
        public DbSet<AnswerForQuestionEntity> AnswersForQuestion { get; set; }
      
        public DbSet<QuestionForTestEntity> QuestionsForTest { get; set; }
        public DbSet<TestForGameEntity> TestsForGame { get; set; }
     
    }
}
