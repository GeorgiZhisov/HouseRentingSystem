namespace House_renting_system_Project.Data.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class HouseRentingDbContextFactory : IDesignTimeDbContextFactory<HouseRentingDbContext>
{
    public HouseRentingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HouseRentingDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=DESKTOP-6E804HH\\SQLEXPRESS;Database=HouseRentingSystem;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

        return new HouseRentingDbContext(optionsBuilder.Options);
    }
}
