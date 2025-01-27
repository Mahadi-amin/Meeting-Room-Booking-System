using DataAccess.Identity;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,
            ApplicationRole, Guid,
            ApplicationUserClaim, ApplicationUserRole,
            ApplicationUserLogin, ApplicationRoleClaim,
            ApplicationUserToken>
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MeetingRoom>(entity =>
            {
                entity.HasKey(mr => mr.Id);

                entity.Property(mr => mr.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(mr => mr.Location)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(mr => mr.Facilities)
                      .HasMaxLength(500);

                entity.Property(mr => mr.Instructions)
                      .HasMaxLength(500);

                entity.Property(mr => mr.Image)
                      .HasMaxLength(255);

                entity.Property(mr => mr.QRCode)
                      .HasMaxLength(255);

            });

            builder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.Purpose)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(b => b.RepeatOption)
                      .HasMaxLength(50);

                entity.HasOne(b => b.MeetingRoom)
                      .WithMany(mr => mr.Bookings) 
                      .HasForeignKey(b => b.MeetingRoomId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            base.OnModelCreating(builder);
        }

        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
