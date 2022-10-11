using CurrencyExchangeTrades.API.Controllers;
using CurrencyExchangeTrades.Service.Dto;
using CurrencyExchangeTrades.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using CurrencyExchangeTrades.API.Configuration;
using CurrencyExchangeTrades.Domain.Enum;
using CurrencyExchangeTrades.Service.ExceptionHandler;
using AutoMapper;

namespace CurrencyExchangeTrades.Test
{
    public class Tests
    {
        TradeController _tradeController;
        CurrencySymbolController _currencySymbolController;
        [SetUp]
        public void Setup()
        {
            BuildConfiguration();
        }

        private void BuildConfiguration()
        {
            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddJsonFile(Environment.CurrentDirectory + @"..\..\..\..\..\CurrencyExchangeTrades.API\appsettings.json", false, true);
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddCustomServices(builder.Configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            builder.Build();
            ITradeService _tradeService = serviceProvider.GetService<ITradeService>();
            _tradeController = new TradeController(_tradeService);
            ICurrencySymbolService _currencySymbolService = serviceProvider.GetService<ICurrencySymbolService>();
            _currencySymbolController = new CurrencySymbolController(_currencySymbolService);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(4)]
        public async Task Trade_Get_by_Id_Test(int id)
        {
            if (id == -1)
                Assert.Null(await _tradeController.Get(id));
            else Assert.NotNull(await _tradeController.Get(id));
        }

        [Test]
        public async Task Trade_Get_All_Test()
        {
            Assert.NotNull(await _tradeController.GetAll());
        }

        [Test]
        public async Task Trade_Set_Success_Test()
        {
            TradeDto trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            string returnValue = await _tradeController.SetTrade(trade);
            Assert.NotNull(returnValue);
            Assert.IsTrue(returnValue.StartsWith($"{trade.Amount} {trade.From} was exchanged for"));
        }

        [Test]
        public async Task Trade_Set_Null_Test()
        {
            TradeDto trade = new TradeDto() { Amount = 1, ClientId = 1, From = null, To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = null, TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );
        }

        [Test]
        public async Task Trade_Set_Invalid_Input_Test()
        {
            TradeDto trade = new TradeDto() { Amount = 1, ClientId = 1, From = "...", To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<NoSymbolException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = "...", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<NoSymbolException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeDto() { Amount = 1, ClientId = 1, From = "...", To = "...", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<NoSymbolException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeDto() { Amount = 1, ClientId = 1, From = ".....", To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = ".....", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeDto() { Amount = 1, ClientId = 1, From = ".....", To = ".....", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );


            trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = "USD", TradeDate = DateTime.Now, TradeType = "..." };
            Assert.ThrowsAsync<AutoMapperMappingException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );
        }

        [Test]
        public async Task TradebyId_Set_Invalid_Input_Test()
        {
            TradeInputDto trade = new TradeInputDto() { Amount = 1, ClientId = 1, From = 0, To = 1, TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeInputDto() { Amount = 1, ClientId = 1, From = 1, To = 0, TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<InvalidCurrencyException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeInputDto() { Amount = 1, ClientId = 1, From = int.MaxValue, To = 1, TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<NoSymbolException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );

            trade = new TradeInputDto() { Amount = 1, ClientId = 1, From = 1, To = int.MaxValue, TradeDate = DateTime.Now, Type = TradeType.Buy };
            Assert.ThrowsAsync<NoSymbolException>(async Task () =>
             await _tradeController.SetTrade(trade)
            );
        }

        [Test]
        public async Task CurrencySymbol_Get_All_Test()
        {
            Assert.NotNull(await _currencySymbolController.GetAll());
        }

        [Test]
        [TestCase(4)]
        public async Task Trade_Get_by_Id_Moq_Test(int id)
        {
            TradeDto trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Mock<ITradeService> tradeService = new();

            tradeService.Setup(x => x.Get(id))
             .ReturnsAsync(trade);

            TradeController controller = new(tradeService.Object);

            Assert.NotNull(await controller.Get(id));
        }


        [Test]
        public async Task Trade_Get_All_Moq_Test()
        {
            TradeDto trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Mock<ITradeService> tradeService = new();

            tradeService.Setup(x => x.GetAll())
             .ReturnsAsync(new List<TradeDto>() { trade });

            TradeController controller = new(tradeService.Object);
            Assert.NotNull(await controller.GetAll());
        }

        [Test]
        public async Task Trade_Set_Moq_Test()
        {
            TradeDto trade = new TradeDto() { Amount = 1, ClientId = 1, From = "EUR", To = "USD", TradeDate = DateTime.Now, Type = TradeType.Buy };
            Mock<ITradeService> tradeService = new();

            tradeService.Setup(x => x.SetTrade(trade))
              .ReturnsAsync("Success!");

            TradeController controller = new(tradeService.Object);
            Assert.NotNull(await controller.SetTrade(trade));
        }


        [Test]
        public async Task CurrencySymbo_Get_All_Moq_Test()
        {
            CurrencySymbolDto currencySymbol = new CurrencySymbolDto() { Symbol = "EUR", Definition = "Euro" };
            Mock<ICurrencySymbolService> currencySymbolService = new();

            currencySymbolService.Setup(x => x.GetSymbols())
             .ReturnsAsync(new List<CurrencySymbolDto>() { currencySymbol });

            CurrencySymbolController controller = new(currencySymbolService.Object);
            Assert.NotNull(await controller.GetAll());
        }
    }
}