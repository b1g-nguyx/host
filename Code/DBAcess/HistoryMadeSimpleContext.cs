using DBAcess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAcess
{
    public class HistoryMadeSimpleContext : IdentityDbContext<ApplicationUser>
    {
        public HistoryMadeSimpleContext(DbContextOptions<HistoryMadeSimpleContext> options)
            : base(options)
        {
        }

        public new DbSet<User>? Users { get; set; } // Added 'new' keyword to explicitly hide the inherited member
        public DbSet<Admin> Admins { get; set; }
        public DbSet<VIPLevel> VIPLevels { get; set; }
        public DbSet<UserVIPSubscription> UserVIPSubscriptions { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonProgress> LessonProgress { get; set; }
        public DbSet<HistoricalSite> HistoricalSites { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<LearningPath> LearningPaths { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<AdminAction> AdminActions { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<AIGeneratedContent> AIGeneratedContent { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<LearningPathLesson> LearningPathLessons { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Flashcard> Flashcards { get; set; }
        public DbSet<LearningMethod> LearningMethods { get; set; }
        public DbSet<LessonMethod> LessonMethods { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<UserVoucher> UserVouchers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính cho LearningPathLessons
            modelBuilder.Entity<LearningPathLesson>()
                .HasKey(lpl => new { lpl.PathID, lpl.LessonID });

            // Đảm bảo UserId trong Admins và Users là duy nhất
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.UserId)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserId)
                .IsUnique();
        }
    }
}
