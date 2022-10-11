using CurrencyExchangeTrades.Domain.Enum;
using CurrencyExchangeTrades.Service.Dto;
using FluentValidation;

namespace CurrencyExchangeTrades.Service.Validators
{
    public class TradeInputValidator : AbstractValidator<TradeInputDto>
    {
        public TradeInputValidator()
        {
            _ = RuleFor(x => x.From).NotEmpty();
            _ = RuleFor(x => x.To).NotEmpty();
            _ = RuleFor(x => x.ClientId).NotEmpty();
            _ = RuleFor(x => x.Amount).NotEmpty();
            _ = RuleFor(x => x.Type).NotNull().IsInEnum();
            _ = RuleFor(x => x.TradeDate).NotNull();
        }

    }
}
