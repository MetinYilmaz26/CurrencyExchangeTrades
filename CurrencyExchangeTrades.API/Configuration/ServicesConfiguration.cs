using AspNetCoreRateLimit;
using CurrencyExchangeTrades.API.Authentication;
using CurrencyExchangeTrades.API.Filter;
using CurrencyExchangeTrades.DataAccess;
using CurrencyExchangeTrades.DataAccess.Repository.Interfaces;
using CurrencyExchangeTrades.DataAccess.Repository.Repositories;
using CurrencyExchangeTrades.Domain.Logic;
using CurrencyExchangeTrades.Service.Dto;
using CurrencyExchangeTrades.Service.Interfaces;
using CurrencyExchangeTrades.Service.Services;
using CurrencyExchangeTrades.Service.Shared.Exchange.Interfaces;
using CurrencyExchangeTrades.Service.Shared.Exchange.Services;
using CurrencyExchangeTrades.Service.TaskServices;
using CurrencyExchangeTrades.Service.TaskServices.Interfaces;
using CurrencyExchangeTrades.Service.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CurrencyExchangeTrades.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            _ = services.AddControllers()
            .AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;
                options.ImplicitlyValidateRootCollectionElements = true;
                _ = options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            _ = services.AddEndpointsApiExplorer();
            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "swagger", Version = "v1" });
                c.SchemaFilter<SchemaFilter>();

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            _ = services.AddLogging();
            _ = services.AddSingleton<IConfiguration>(configuration);
            _ = services.AddAuthentication("BasicAuthentication")
                            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
                            ("BasicAuthentication", null);
            _ = services.AddDbContext<CurrencyExchangeTradesDBContext>(c => new CurrencyExchangeTradesDBContext(configuration));
            _ = services.AddMemoryCache();
            _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            _ = services.AddTransient<ITradeRepository, TradeRepository>();
            _ = services.AddTransient<ITradeService, TradeService>();
            _ = services.AddTransient<ICurrencySymbolRepository, CurrencySymbolRepository>();
            _ = services.AddTransient<ICurrencySymbolService, CurrencySymbolService>();
            _ = services.AddTransient<IExchangeService, ExchangeService>();
            _ = services.AddTransient<IRestService, RestService>();
            _ = services.AddTransient<ITradeLogic, TradeLogic>();
            _ = services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
            _ = services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            _ = services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            _ = services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            _ = services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            _ = services.AddInMemoryRateLimiting();
            _ = services.AddHostedService<ConsumeScopedServiceHostedService>();
            _ = services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            _ = services.AddScoped<IValidator<TradeDto>, TradeValidator>();
            _ = services.AddScoped<IValidator<TradeInputDto>, TradeInputValidator>();
        }
    }
}
