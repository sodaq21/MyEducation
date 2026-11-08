using ASPNET_Practice.Models;
using System.ComponentModel.DataAnnotations;

namespace ASPNET_Practice.DTOs
{
    public class CreateCarRequest
    {
        [Required]
        [StringLength(30)]
        public string Brand { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [Required]
        public DateOnly ProductionDate { get; set; }

        public int? HorsePower { get; set; }

        public CarBodyType? BodyType{ get; set; }
    }
}
