using ExamifyApis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ExamifyApis.DB
{
    public class DBContextClass : IdentityDbContext<ApplicationUser>
    {
        public DBContextClass(DbContextOptions<DBContextClass> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);




            /*Course Relations*/

            // Course - Teacher (Many-to-One)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            // Course - Student (Many-to-Many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students)
                .WithMany(s => s.Courses)
                .UsingEntity(j => j.ToTable("CourseStudent"));

            // Course - Exam (One-to-Many)
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.NoAction);

            // Course - Grade (One-to-Many)



            /*Stdeunt Relations*/

            // Student - Answer (One-to-Many)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.StudentAttempts)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.NoAction);


            /*Exam Relations*/
            // Exam - Question (One-to-Many)
            modelBuilder.Entity<Exam>()
            .HasMany(e => e.Questions)
            .WithOne(q => q.Exam)
            .HasForeignKey(q => q.ExamId)
            .OnDelete(DeleteBehavior.Cascade);

            // Exam - Grade (One-to-Many)
            modelBuilder.Entity<Exam>()
                    .HasMany(e => e.StudentAttempts)
                    .WithOne(g => g.Exam)
                    .HasForeignKey(g => g.ExamId)
                    .OnDelete(DeleteBehavior.NoAction);


            /*Grade Relations*/



            /*Answer Relations*/



            // Answer - Question (Many-to-One)
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            /*Question Relations*/

            // Question - Answer (One-to-Many)
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.NoAction);


            /*Teacher Relations*/
            // Teacher - Course (One-to-Many)
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.NoAction);

            /*Attempt Relations*/
            // Attempt - Answer (Many-to-one)
            modelBuilder.Entity<Attempt>()
                .HasMany(a => a.Answers)
                .WithOne(a => a.Attempt)
                .HasForeignKey(a => a.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            // Attempt - Grade (One-to-One)
            modelBuilder.Entity<Attempt>()
                    .HasOne(a => a.Grade)
                    .WithOne(g => g.Attempt)
                    .HasForeignKey<Grade>(g => g.AttemptId)
                    .OnDelete(DeleteBehavior.Cascade);


            // StudentAttempts - Attempt (Many-to-One)
            modelBuilder.Entity<StudentAttempts>()
                    .HasMany(sa => sa.Attempts)
                    .WithOne(a => a.StudentAttempts)
                    .HasForeignKey(sa => sa.StudentAttemptsId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentAttempts>()
                .HasOne(sa => sa.Student)
                    .WithMany(s => s.StudentAttempts)
                    .HasForeignKey(sa => sa.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentAttempts>()
                .HasOne(sa => sa.Exam)
                    .WithMany(e => e.StudentAttempts)
                    .HasForeignKey(sa => sa.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set;}
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<StudentAttempts> StudentAttempts { get; set; }

    }
}
