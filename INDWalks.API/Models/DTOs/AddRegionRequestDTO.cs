using System.ComponentModel.DataAnnotations;

namespace INDWalks.API.Models.DTOs
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Region Name must be within 100 characters")]
        public string Name { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Region Code must be of 3 characters")]
        [MaxLength(3, ErrorMessage = "Region Code must be of 3 characters")]
        public string Code { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
