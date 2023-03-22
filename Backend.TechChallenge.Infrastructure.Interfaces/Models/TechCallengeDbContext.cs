using Microsoft.EntityFrameworkCore;

namespace Backend.TechChallenge.Infrastructure.Interfaces.Models
{
    public partial class TechCallengeDbContext : DbContext
    {
        public TechCallengeDbContext()
        {
        }

        public TechCallengeDbContext(DbContextOptions<TechCallengeDbContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Guid)
                    .HasColumnName("Guid")
                    .HasDefaultValueSql("newid()");

                entity.Property(e => e.Name)
                            .IsRequired()
                            .HasMaxLength(50)
                            .HasColumnName("Name");

                entity.Property(e => e.Email)
                            .IsRequired()
                            .HasMaxLength(100)
                            .HasColumnName("Email");

                entity.Property(e => e.Address)
                            .IsRequired()
                            .HasMaxLength(250)
                            .HasColumnName("Address");

                entity.Property(e => e.Phone)
                            .IsRequired()
                            .HasMaxLength(15)
                            .HasColumnName("Phone");

                entity.Property(e => e.UserType)
                           .IsRequired()
                           .HasColumnName("UserType");

                entity.Property(e => e.Money)
                            .IsRequired()
                            .HasColumnName("Money");


                entity.Property(e => e.InsertedDate)
                            .IsRequired()
                            .HasColumnName("InsertedDate")
                            .HasDefaultValueSql("getutcdate()");

                entity.Property(e => e.UpdatedDate)
                            .IsRequired(false)   
                            .HasColumnName("UpdatedDate");

                entity.Property(e => e.IsDeleted)
                            .IsRequired()
                            .HasColumnName("IsDeleted")
                            .HasDefaultValue(false);
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
