namespace House_renting_system_Project.Data.Data;

using System.Reflection;
using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class HouseRentingDbContext(
	DbContextOptions<HouseRentingDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<House> Houses { get; init; } = null!;

	public DbSet<Category> Categories { get; init; } = null!;

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseSqlServer(
				"Server=DESKTOP-6E804HH\\SQLEXPRESS;Database=HouseRentingSystem;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");
		}
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfigurationsFromAssembly(
			Assembly.GetExecutingAssembly());
	}
}
