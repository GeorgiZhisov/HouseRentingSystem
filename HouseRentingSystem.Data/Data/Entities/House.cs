using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HouseRentingSystem.Data.Data.DataConstants.House;

namespace HouseRentingSystem.Data.Data.Entities
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(TitleMaxLength)]
        [Required]
        public string Title { get; set; } = null!;

        [MaxLength(AddressMaxLenght)]
        [MinLength(30)]
        [Required]
        public string Address { get; set; } = null!;
        [MaxLength(DescriptionMaxLength)]
        [MinLength(50)]
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public string ImageUrl { get; set; } = null!;

        [Column(TypeName = "decimal(12,3)")]
        [Required]
        public decimal PricePerMonth { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int AgentId { get; set; } 
        public Agent Agent { get; set; } = null!;

        public string? RenterId { get; set; }
        public IdentityUser? Renter { get; set; } = null!;
    }
}
