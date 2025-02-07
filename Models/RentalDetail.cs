using VideoShopRentalV3.Models;

public class RentalDetail
{
    public int RentalDetailId { get; set; }
    public int RentalHeaderId { get; set; }
    public int MovieId { get; set; }

    public RentalHeader? RentalHeader { get; set; }
    public Movie? Movie { get; set; }
}