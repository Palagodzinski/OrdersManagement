using Microsoft.EntityFrameworkCore;
using OrdersManagement.Domain.Entities;

namespace OrdersManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BillingEntry> BillingEntries { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Offer> Offers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(x =>
            {
                x.ToTable("OrderTable", "dbo");

                x.Property(x => x.Id)
                 .ValueGeneratedOnAdd();

                x.HasKey(x => x.Id)
                 .HasName("PK_panel_lista");

                x.HasIndex(x => new { x.OrderId, x.StoreId })
                 .IsUnique()
                 .HasDatabaseName("si");

                x.Property(x => x.OrderId)
                 .IsRequired()
                 .HasMaxLength(45);

                x.Property(x => x.ErpOrderId)
                 .IsRequired(false);

                x.Property(x => x.InvoiceId)
                 .IsRequired(false);

                x.Property(x => x.StoreId)
                 .IsRequired(false);
            });

            modelBuilder.Entity<BillingEntry>(x =>
            {
                x.ToTable("BillingEntryTable", "dbo");

                x.Property(x => x.Id)
               .ValueGeneratedOnAdd();

                x.Property(x => x.BillingEntryId)
                .IsRequired();

                x.HasKey(x => x.Id)
                .HasName("PK_billing_lista");

                x.HasIndex(x => x.OrderId)
                .HasDatabaseName("IDX_BillingEntryTable_OrderId");

                x.HasOne(x => x.Order)
                 .WithMany(x => x.BillingEntries)
                 .HasForeignKey(x => x.OrderId)
                 .OnDelete(DeleteBehavior.Restrict);

                x.Property(x => x.OrderId)
                .IsRequired();

                x.HasOne(x => x.Offer)
                .WithMany(x => x.BillingEntries)
                .HasForeignKey(x => x.OfferId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Offer>(x =>
            {
                x.ToTable("OfferTable", "dbo");
                x.Property(x => x.Id)
                .ValueGeneratedOnAdd();

                x.HasKey(x => x.Id)
                .HasName("PK_offer_lista");
            });
        }
    }
}
