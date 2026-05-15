using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Data.Data.Entities.Configuration
{
    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {

            builder
                .HasOne(h => h.Category)
                .WithMany(c => c.Houses)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder
                .HasOne(h => h.Agent)
                .WithMany(a => a.Houses)
                .HasForeignKey(h => h.AgentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(SeedHouses());
            builder.HasQueryFilter(h => h.IsDeleted == false);


        }

        private IEnumerable<House> SeedHouses()
        {
            return new House[]
            {
        new House()
        {
            Id = 1,
            Title = "Big House Marina",
            Address = "North London, UK (near the border)",
            Description = "A big marina house for your whole family. Perfect place with a beautiful view and lots of space.",
            ImageUrl = "https://www.thegoodlifebahamasrentals.com/wp-content/uploads/2024/05/Marina-House-Exuma-Great-Exuma-1.webp",
            PricePerMonth = 2100.00M,
            CategoryId = 2,
            AgentId = "13d2281e-1912-45f4-8aef-9c1288ac7fbc"
        },
        new House()
        {
            Id = 2,
            Title = "Big Family House",
            Address = "Near the Sea Garden in Burgas, Bulgaria",
            Description = "A big family house with enough space, comfort and privacy for the whole family.",
            ImageUrl = "https://www.lanciahomes.com/sites/default/files/blog-files/lancia_homes_single-family_home_what_it_actually_is_image1.jpeg",
            PricePerMonth = 1200.00M,
            CategoryId = 2,
            AgentId = "13d2281e-1912-45f4-8aef-9c1288ac7fbc"
        }
            };
        }
    }
    
}
