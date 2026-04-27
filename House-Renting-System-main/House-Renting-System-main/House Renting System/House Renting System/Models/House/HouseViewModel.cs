namespace House_Renting_System.Models.House
{
    public class HousesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public bool CurentUserIsOwner { get; set; }
    }
}