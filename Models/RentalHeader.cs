namespace VideoShopRentalV3.Models
{
    public class RentalHeader
    {
        public int RentalHeaderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public List<int>? MovieIds { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
        public ICollection<RentalDetail> RentalDetails { get; set; } = new List<RentalDetail>();
    }
}
