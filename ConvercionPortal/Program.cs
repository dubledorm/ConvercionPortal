using ConvercionPortal.Services;
using Data.Stores.Cainiao;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

const string ConfigSectionName = "CainiaoStatusesDB";
const string ConfigDBUrl = "DatabaseUrl";
const string ConfigDBPort = "DatabasePort";
const string ConfigDBName = "DatabaseName";

// Создаём логгер для использования в Program.cs
var config = new ConfigurationBuilder()
       .SetBasePath(System.IO.Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false).Build();

LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
var logger = NLog.Web.NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();

try
{
    logger.Info("ConvercionPortal Сервис запущен");
    var builder = WebApplication.CreateBuilder(args);

    // Передаём конфигурацию для использования логгера через DI
    builder.Host.ConfigureLogging(logging =>
     {
         logging.ClearProviders();
         logging.ConfigureNLog(new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog")));
     }).UseNLog();

    

    // Add services to the container.

    builder.Services.AddRazorPages();
    
    builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(
                new MongoClientSettings()
                {
                    Server = new MongoServerAddress(builder.Configuration.GetSection(ConfigSectionName)[ConfigDBUrl],
                                                    int.Parse(builder.Configuration.GetSection(ConfigSectionName)[ConfigDBPort])),
                    ClusterConfigurator = cb =>
                    {
                        cb.Subscribe<CommandStartedEvent>(e =>
                        {
                            // _logger.LogDebug($"{e.CommandName} - {e.Command.ToJson()}");
                        });
                    }
                }

                ));
    builder.Services.AddSingleton<IMongoDatabase>(sp =>
    {
        var client = sp.GetRequiredService<IMongoClient>();
        return client.GetDatabase(builder.Configuration.GetSection(ConfigSectionName)[ConfigDBName]);
    });

    builder.Services.AddScoped<IEncloseEventsStore, MongoEncloseEventsStore>();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
    return 0;

}
catch (Exception exception) {
  logger.Fatal($"ERROR Необработанная ошибка. Message - {exception.Message}. InnerException message = {exception.InnerException?.Message ?? "null"}");
  return 1;
}
finally
{
   logger.Info("ConvercionPortal остановлен");
}


