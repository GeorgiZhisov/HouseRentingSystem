using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HouseRentingSystem.Data.Data.DataConstants.Category;

namespace HouseRentingSystem.Data.Data.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(NameMaxLength)]

        public string Name { get; set; } = null!;
        public IEnumerable<House> Houses { get; set; } = new List<House>();

    }
}
