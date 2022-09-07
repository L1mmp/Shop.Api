using Microsoft.EntityFrameworkCore;
using Shop.Domain.Entities;

namespace Shop.DataAccess
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{ }
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Cart> Carts { get; set; } = null!;
		public DbSet<Item> Items { get; set; } = null!;
		public DbSet<Order> Orders { get; set; } = null!;
		public DbSet<OrderItem> OrderItems { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasAlternateKey("Login");
		}
	}
}
