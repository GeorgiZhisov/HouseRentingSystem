using HouseRentingSystem.Data.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Data.Configuration
{
    public class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        { }
            private IEnumerable<House> SeedHouses()
            {
           return new House[]
            {
                new House{
                Id = 1,
                Title = "House",
                Address = "Pirotska 139",
                Description = "party club Elmo",
                ImageUrl = "https://lh3.googleusercontent.com/gps-cs-s/AHVAweqCiRfKB_PuUmg3CbU44JLz0V5xQCwmUZY3mIomCDa9JeJRDiqoy9WhwhBFERRnprN495wCqe1Ci8vXcc3UJtn329RDiqWPgwK24ErL8c3sceK0yIrcLfn02JeC8MlpAKPSY6UA=w243-h174-n-k-no-nu ",
                PricePerMonth = 109.00M,
                CategoryId = 2,
                AgentId = 2,
                }
            };
            } 
        
    }
}
