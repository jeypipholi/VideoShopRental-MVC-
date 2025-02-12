using System.Text.Json.Serialization;

public class Movie
{
    public int MovieId { get; set; }
    public string? Title { get; set; }
    public string? Genre { get; set; }
    public string? ReleaseYear { get; set; }
    public decimal RentalPrice { get; set; }

    [JsonIgnore]
    public ICollection<RentalDetail>? RentalDetails { get; set; }
}