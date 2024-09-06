using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    public class AlzaDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }

        public AlzaDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // umožňuje vyhledat objednávku dle jejího stavu 
            modelBuilder
                .Entity<Order>()
                .Property(o => o.OrderState)
                .HasConversion<int>();

            // konvertuje enum na string hodnotu pro přehlednější zápis do DB
            modelBuilder
                .Entity<Order>()
                .Property(o => o.OrderState)
                .HasConversion(new EnumToStringConverter<OrderState>());

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.ItemId });

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Item)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.ItemId);

            modelBuilder.Entity<Customer>().HasData(
               new Customer { Id = 1, CustomerName = "Alza s.r.o", Password = "ŠelDědečekNaKopeček" },
               new Customer { Id = 2, CustomerName = "John", CustomerSurname = "Doe", Password = "TrhalaFialkyDynamitem" },
               new Customer { Id = 3, CustomerName = "Aleš", CustomerSurname = "Zavoral", Password = "ZavoralJeBuhNaZemi" }
             );

            // Seedování dat pro entitu Order
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderNumber = "ORD001",
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    OrderState = OrderState.New
                },
                new Order
                {
                    Id = 2,
                    OrderNumber = "ORD002",
                    CustomerId = 2,
                    OrderDate = DateTime.Now.AddDays(-1),
                    OrderState = OrderState.New
                }
            );

            // Seedování dat pro entitu Item
            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, ItemName = "Laptop", NumberOfItems = 10, Price = 1500.00m },
                new Item { Id = 2, ItemName = "Mouse", NumberOfItems = 50, Price = 25.00m },
                new Item { Id = 3, ItemName = "Keyboard", NumberOfItems = 30, Price = 75.00m }
            );

            modelBuilder.Entity<OrderItem>().HasData(
               new OrderItem { OrderId = 1, ItemId = 1 },
               new OrderItem { OrderId = 1, ItemId = 2 },
               new OrderItem { OrderId = 2, ItemId = 2 },
               new OrderItem { OrderId = 2, ItemId = 3 }
           );
        }
    }
}
