using System.ComponentModel.DataAnnotations;

namespace DialInApi.Models
{
    public class DialIn
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DialInId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string? BrewMethod { get; set; }

        [Required]
        public string? TimeStamp { get; set; }

        [Required]
        public string? CoffeeName { get; set; }

        public string? CoffeeCode { get; set; }

        public float AmountInGrams { get; set; }
        public float YieldInGrams { get; set; }
        public int ShotDuration { get; set; }

        [Required]
        public string? GrindSize { get; set; }

        [Required]
        public string? GrinderName { get; set; }

        [Required]
        public string? TasteResult { get; set; }
    }
}