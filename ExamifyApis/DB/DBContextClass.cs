using ExamifyApis.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ExamifyApis.DB
{
    public class DBContextClass : DbContext
    {
        public DBContextClass(DbContextOptions<DBContextClass> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
                    .HasMany(s => s.Answers)
                    .WithOne(a => a.Student)
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

                // Student - Grade (One-to-Many)
                modelBuilder.Entity<Student>()
                    .HasMany(s => s.Grades)
                    .WithOne(g => g.Student)
                    .HasForeignKey(g => g.StudentId)
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
                    .HasMany(e => e.Grades)
                    .WithOne(g => g.Exam)
                    .HasForeignKey(g => g.ExamId)
                    .OnDelete(DeleteBehavior.NoAction);


            /*Grade Relations*/


                // Grade - Exam (Many-to-One)
                modelBuilder.Entity<Grade>()
                    .HasOne(g => g.Exam)
                    .WithMany(e => e.Grades)
                    .HasForeignKey(g => g.ExamId)
                    .OnDelete(DeleteBehavior.NoAction);

                // Grade - Student (Many-to-One)
                modelBuilder.Entity<Grade>()
                    .HasOne(g => g.Student)
                    .WithMany(s => s.Grades)
                    .HasForeignKey(g => g.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);

            /*Answer Relations*/



                // Answer - Student (Many-to-One)
                modelBuilder.Entity<Answer>()
                    .HasOne(a => a.Student)
                    .WithMany(s => s.Answers)
                    .HasForeignKey(a => a.StudentId)
                    .OnDelete(DeleteBehavior.NoAction);



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
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public DbSet<User> Users { get; set;}
    }
}
