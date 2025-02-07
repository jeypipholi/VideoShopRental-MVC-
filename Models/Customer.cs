using Microsoft.Build.Framework;

namespace VideoShopRentalV3.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }           // Primary Key
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        public required string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        
        public ICollection<RentalHeader>? RentalHeaders { get; set; }
    }
}
