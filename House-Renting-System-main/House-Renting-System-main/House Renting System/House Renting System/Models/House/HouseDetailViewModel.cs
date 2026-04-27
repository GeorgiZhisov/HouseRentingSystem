namespace House_Renting_System.Models.House
{
    public class HouseDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string CreatedBy { get; set; } = null!;

        public bool CurentUserIsOwner { get; set; }
    }
}