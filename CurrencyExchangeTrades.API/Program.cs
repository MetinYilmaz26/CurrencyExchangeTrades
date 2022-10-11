using AspNetCoreRateLimit;
using CurrencyExchangeTrades.API.Configuration;
using CurrencyExchangeTrades.Service.ExceptionHandler;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
builder.Logging.AddSerilog();

builder.Services.AddCustomServices(builder.Configuration);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseIpRateLimiting();

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = Text.Plain;
        await context.Response.WriteAsync(UserFriendlyException.CatchException(exceptionHandlerPathFeature?.Error));
    });
});

IIpPolicyStore IpPolicy = app.Services.GetRequiredService<IIpPolicyStore>();
await IpPolicy.SeedAsync();
await app.RunAsync();
