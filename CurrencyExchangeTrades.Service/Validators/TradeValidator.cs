using CurrencyExchangeTrades.Service.Dto;
using FluentValidation;

namespace CurrencyExchangeTrades.Service.Validators
{
    public class TradeValidator : AbstractValidator<TradeDto>
    {
        public TradeValidator()
        {
            _ = RuleFor(x => x.From).NotNull().Length(3);
            _ = RuleFor(x => x.To).NotNull().Length(3);
            _ = RuleFor(x => x.ClientId).NotEmpty();
            _ = RuleFor(x => x.Amount).NotEmpty();
            _ = RuleFor(x => x.TradeType).NotNull().Length(3, 4);
            _ = RuleFor(x => x.TradeDate).NotNull();
        }

    }
}
