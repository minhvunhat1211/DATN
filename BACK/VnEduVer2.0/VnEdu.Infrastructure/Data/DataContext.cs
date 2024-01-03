using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using VnEdu.Core.Entities.Models;

namespace VnEdu.Infrastructure.Data
{
    /// <summary>
    /// Information of DataContext
    /// CreatedBy: MinhVN(20/12/2022)
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Class
        /// </summary>
        public DbSet<Class> Class { get; set; } = default!;

        /// <summary>
        /// Decentralization
        /// </summary>
        public DbSet<Decentralization> Decentralization { get; set; } = default!;

        /// <summary>
        /// DetailedTableScore
        /// </summary>
        public DbSet<DetailedTableScore> DetailedTableScore { get; set; } = default!;

        /// <summary>
        /// SchoolYear
        /// </summary>
        public DbSet<SchoolYear> SchoolYear { get; set; } = default!;

        /// <summary>
        /// Semester
        /// </summary>
        public DbSet<Semester> Semester { get; set; } = default!;

        /// <summary>
        /// Student
        /// </summary>
        public DbSet<Student> Student { get; set; } = default!;

        /// <summary>
        /// Student_Class
        /// </summary>
        public DbSet<Student_Class> Student_Class { get; set; } = default!;

        /// <summary>
        /// Student_Subject
        /// </summary>
        public DbSet<Student_Subject> Student_Subject { get; set; } = default!;

        /// <summary>
        /// Subject
        /// </summary>
        public DbSet<Subject> Subject { get; set; } = default!;

        /// <summary>
        /// Teacher
        /// </summary>
        public DbSet<Teacher> Teacher { get; set; } = default!;

        /// <summary>
        /// Teacher_Class
        /// </summary>
        public DbSet<Teacher_Class> Teacher_Class { get; set; } = default!;

        /// <summary>
        /// Teacher_Subject
        /// </summary>
        public DbSet<Teacher_Subject> Teacher_Subject { get; set; } = default!;

        /// <summary>
        /// User
        /// </summary>
        public DbSet<User> User { get; set; } = default!;

        /// <summary>
        /// OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        /// CreatedBy: MinhVN(20/12/2022)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student_Class>().HasKey(sc => new { sc.StudentId, sc.ClassId, sc.SemesterId, sc.SchoolYearId });
            modelBuilder.Entity<Teacher_Class>().HasKey(tc => new { tc.TeacherId, tc.ClassId, tc.SemesterId, tc.SchoolYearId });
            modelBuilder.Entity<Teacher_Subject>().HasKey(ts => new { ts.TeacherId, ts.SubjectId, ts.SemesterId, ts.SchoolYearId });
            modelBuilder.Entity<Student_Subject>().HasKey(ss => new { ss.DetailedTableScoreId});
        }
    }
}