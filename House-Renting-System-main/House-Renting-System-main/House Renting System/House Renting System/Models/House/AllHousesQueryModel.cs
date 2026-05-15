using House_Renting_System.Models.House.Helpers;
using System.ComponentModel.DataAnnotations;

namespace House_Renting_System.Models.House
{
    public class AllHousesQueryModel
    {
        public const int HousesPerPage = 3;

        public string? Category { get; set; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; set; }

        public HouseSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalHousesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = new List<string>();

        public IEnumerable<HousesViewModel> Houses { get; set; } = new List<HousesViewModel>();
    }
}