using Microsoft.EntityFrameworkCore;
using PrivateTutoringApplication.Model.Entity;

namespace PrivateTutoringApplication.Model.Infrastructure
{
    public sealed partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Giris> Girisler { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonComment> LessonComments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TutorLesson> TutorLessons { get; set; }
        public DbSet<TutorSchedule> TutorSchedules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=PrivateTutoringApplication;Username=postgres;Password=Cankaya2014");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}
