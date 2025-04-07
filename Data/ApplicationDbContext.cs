//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Campus_Circle.Models;

//namespace Campus_Circle.Data
//{
//    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options) { }

//        public DbSet<PG> PGs { get; set; }
//        public DbSet<Booking> Bookings { get; set; }
//        public DbSet<Payment> Payments { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // ✅ Booking Configuration
//            modelBuilder.Entity<Booking>(entity =>
//            {
//                entity.HasKey(b => b.BookingId); // प्राइमरी की
//                entity.Property(b => b.BookingId)
//                      .UseIdentityColumn(); // IDENTITY प्रॉपर्टी

//                // डेसीमल प्रॉपर्टीज को स्पष्ट रूप से कॉन्फ़िगर करें
//                entity.Property(b => b.TotalAmount)
//                      .HasColumnType("decimal(18,2)");

//                entity.Property(b => b.PGPrice)
//                      .HasColumnType("decimal(18,2)")
//                      .HasDefaultValue(0.0m); // डिफ़ॉल्ट वैल्यू

//                // रिलेशनशिप्स
//                entity.HasOne(b => b.User)
//                      .WithMany(u => u.Bookings)
//                      .HasForeignKey(b => b.UserId)
//                      .OnDelete(DeleteBehavior.Cascade);

//                entity.HasOne(b => b.PG)
//                      .WithMany(p => p.Bookings)
//                      .HasForeignKey(b => b.PGId)
//                      .OnDelete(DeleteBehavior.Cascade);
//            });

//            // ✅ Payment Configuration
//            modelBuilder.Entity<Payment>(entity =>
//            {
//                entity.HasKey(p => p.PaymentId);
//                entity.Property(p => p.Amount)
//                      .HasColumnType("decimal(18,2)");
//            });

//            // ✅ PG Configuration
//            modelBuilder.Entity<PG>(entity =>
//            {
//                entity.HasIndex(p => p.Name).IsUnique(); // यूनिक नाम
//            });
//        }
//    }
//}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Campus_Circle.Models;

namespace Campus_Circle.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<PG> PGs { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Booking Configuration (Remove Foreign Keys)
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.BookingId);
                entity.Property(b => b.BookingId)
                      .UseIdentityColumn();

                entity.Property(b => b.TotalAmount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(b => b.PGPrice)
                      .HasColumnType("decimal(18,2)")
                      .HasDefaultValue(0.0m);

                // ❌ Removed Foreign Key Relationships
                // entity.HasOne(b => b.User).WithMany(u => u.Bookings).HasForeignKey(b => b.UserId);
                // entity.HasOne(b => b.PG).WithMany(p => p.Bookings).HasForeignKey(b => b.PGId);
            });

            // ✅ Payment Configuration (Remove Foreign Key)
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.PaymentId);
                entity.Property(p => p.Amount)
                      .HasColumnType("decimal(18,2)");

                // ❌ Removed Foreign Key Relationship
                // entity.HasOne(p => p.Booking).WithMany(b => b.Payments).HasForeignKey(p => p.BookingId);
            });

            // ✅ PG Configuration
            modelBuilder.Entity<PG>(entity =>
            {
                entity.HasIndex(p => p.Name).IsUnique();
            });
        }
    }
}
