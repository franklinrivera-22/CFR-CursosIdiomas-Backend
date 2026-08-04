using CursosApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CursosApp.Database
{
 public class AppDbContext : IdentityDbContext<UserEntity, RoleEntity, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SetIdentityTableNames(builder);

            builder.Entity<CourseEntity>().Property(c => c.Price).HasPrecision(18, 2);
            builder.Entity<TransactionEntity>().Property(t => t.Amount).HasPrecision(18, 2);
            builder.Entity<TransactionItemEntity>().Property(i => i.UnitPrice).HasPrecision(18, 2);

            builder.Entity<TransactionEntity>()
                .HasMany(t => t.Items)
                .WithOne(i => i.Transaction)
                .HasForeignKey(i => i.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<EnrollmentEntity>()
                .HasOne(e => e.Course)
                .WithMany()
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private static void SetIdentityTableNames(ModelBuilder builder)
        {
            builder.Entity<UserEntity>().ToTable("users");
            builder.Entity<RoleEntity>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("users_roles")
                .HasKey(ur => new { ur.UserId, ur.RoleId });
            builder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("users_login");
            builder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");
        }

        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<TransactionItemEntity> TransactionItems { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
    }
}