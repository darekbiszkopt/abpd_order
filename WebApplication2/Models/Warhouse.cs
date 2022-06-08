using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Warhouse
    {
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdWarehouse { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Amount should be greater than 0!")]
        public int Amount { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
