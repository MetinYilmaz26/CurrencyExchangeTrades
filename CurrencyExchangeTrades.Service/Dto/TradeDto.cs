using CurrencyExchangeTrades.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CurrencyExchangeTrades.Service.Dto
{
    public class TradeDto
    {
        public int ClientId { get; set; }
        public DateTime TradeDate { get; set; }
        [IgnoreDataMember]
        public int FromId { get; set; }
        [StringLength(3)]
        public string From { get; set; }
        [IgnoreDataMember]
        public string? FromDefinition { get; set; }
        [IgnoreDataMember]
        public int ToId { get; set; }
        [StringLength(3)]
        public string To { get; set; }
        [IgnoreDataMember]
        public string? ToDefinition { get; set; }
        public double Amount { get; set; }
        [IgnoreDataMember]
        public double Rate { get; set; }
        public string TradeType { get; set; }
        [IgnoreDataMember]
        public TradeType Type { get; set; }
    }
    public class TradeInputDto
    {
        public int ClientId { get; set; }
        public DateTime TradeDate { get; set; }
        public int From { get; set; }
        [StringLength(3)]
        [IgnoreDataMember]
        public string? FromSymbol { get; set; }
        public int To { get; set; }
        [StringLength(3)]
        [IgnoreDataMember]
        public string? ToSymbol { get; set; }
        public double Amount { get; set; }
        [IgnoreDataMember]
        public double Rate { get; set; }
        public TradeType Type { get; set; }
    }
}
