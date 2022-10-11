using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CurrencyExchangeTrades.Domain.Enum;

namespace CurrencyExchangeTrades.Domain.Entity
{
    public class Trade : BaseEntity
    {
        [Required]
        public int ClientId { get; set; }
        [Required]
        public DateTime TradeDate { get; set; }
        [ForeignKey("CurrencySymbol")]
        [Required]
        public int FromId { get; set; }
        public CurrencySymbol From { get; set; }
        [ForeignKey("CurrencySymbol")]
        [Required]
        public int ToId { get; set; }
        public CurrencySymbol To { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public TradeType Type { get; set; }
    }
}
