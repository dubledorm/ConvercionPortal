using ConvercionPortal.Services;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

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

    builder.Services.AddDbContextPool<AppDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("CustomerDBConnection"));
    });
    builder.Services.AddRazorPages();
    builder.Services.AddScoped<ICustomerRepository, SQLCustomerRepository>();
    builder.Services.AddSingleton<IConvercionTypeRepository, MockConvercionTypeRepository>();
    //builder.Services.AddSingleton<IEncloseAndCNStatusRepository, MockEncloseAndCNStatusRepository>();
    builder.Services.AddScoped<ICnEncloseStatusRepository, MongoCnEncloseStatusRepository>();


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


