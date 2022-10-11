using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CurrencyExchangeTrades.Domain.Entity
{
    public class CurrencySymbol : BaseEntity
    {
        [StringLength(3)]
        [Required]
        public string Symbol { get; set; }
        [StringLength(50)]
        [Required]
        public string Definition { get; set; }
    }
}
