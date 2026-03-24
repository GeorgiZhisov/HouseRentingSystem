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
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        { }

        private IEnumerable<Agent> SeedAgents()
        {
            return new Agent[]
            {
                new Agent
                {
                    Id = 1,
                    PhoneNumber = "0888888888",
                    UserId = "user-id-1"
                },
                new Agent
                {
                    Id = 2,
                    PhoneNumber = "0899999999",
                    UserId = "user-id-2"
                }
            };
        }
    }
}