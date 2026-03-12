using MermaidDotNet.EntityFramework.Tests.Mock.Entities;
using System.Data.Common;
using System.Data.Entity;

namespace MermaidDotNet.EntityFramework.Tests.Mock
{
    public class DatabaseContextMock : DbContext
    {
        public DatabaseContextMock() : base("name=DefaultConnection")
        {
        }

        public DatabaseContextMock(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DatabaseContextMock(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<SchoolClass> SchoolClasses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Enrollment> Enrollments { get; set; }
        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Submission> Submissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Student configuration
            modelBuilder.Entity<Student>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Student>()
                .Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Student>()
                .Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Student>()
                .Property(s => s.Address.Street)
                .HasColumnName("Address_Street")
                .HasMaxLength(200);

            modelBuilder.Entity<Student>()
                .Property(s => s.Address.City)
                .HasColumnName("Address_City")
                .HasMaxLength(100);

            modelBuilder.Entity<Student>()
                .Property(s => s.Address.PostalCode)
                .HasColumnName("Address_PostalCode")
                .HasMaxLength(10);

            modelBuilder.Entity<Student>()
                .HasRequired(s => s.SchoolClass)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.SchoolClassId)
                .WillCascadeOnDelete(false);

            // Teacher configuration
            modelBuilder.Entity<Teacher>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Teacher>()
                .Property(t => t.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Teacher>()
                .Property(t => t.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Teacher>()
                .Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(255);

            // SchoolClass configuration
            modelBuilder.Entity<SchoolClass>()
                .HasKey(sc => sc.Id);

            modelBuilder.Entity<SchoolClass>()
                .Property(sc => sc.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<SchoolClass>()
                .HasRequired(sc => sc.Teacher)
                .WithMany(t => t.SchoolClasses)
                .HasForeignKey(sc => sc.TeacherId)
                .WillCascadeOnDelete(false);

            // Course configuration
            modelBuilder.Entity<Course>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Course>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Course>()
                .Property(c => c.Description)
                .IsOptional()
                .HasMaxLength(1000);

            modelBuilder.Entity<Course>()
                .HasRequired(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .WillCascadeOnDelete(false);

            // Enrollment configuration
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.EnrolledAt)
                .IsRequired();

            modelBuilder.Entity<Enrollment>()
                .Property(e => e.Grade)
                .IsOptional()
                .HasPrecision(5, 2);

            modelBuilder.Entity<Enrollment>()
                .HasRequired(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Enrollment>()
                .HasRequired(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(true);

            // Assignment configuration
            modelBuilder.Entity<Assignment>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Assignment>()
                .Property(a => a.Title)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Assignment>()
                .Property(a => a.Description)
                .IsOptional()
                .HasMaxLength(2000);

            modelBuilder.Entity<Assignment>()
                .HasRequired(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId)
                .WillCascadeOnDelete(true);

            // Submission configuration
            modelBuilder.Entity<Submission>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Submission>()
                .Property(s => s.SubmittedAt)
                .IsRequired();

            modelBuilder.Entity<Submission>()
                .Property(s => s.Grade)
                .IsOptional()
                .HasPrecision(5, 2);

            modelBuilder.Entity<Submission>()
                .Property(s => s.Comment)
                .IsOptional()
                .HasMaxLength(2000);

            modelBuilder.Entity<Submission>()
                .HasRequired(s => s.Assignment)
                .WithMany(a => a.Submissions)
                .HasForeignKey(s => s.AssignmentId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Submission>()
                .HasRequired(s => s.Student)
                .WithMany(st => st.Submissions)
                .HasForeignKey(s => s.StudentId)
                .WillCascadeOnDelete(false);
        }
    }
}