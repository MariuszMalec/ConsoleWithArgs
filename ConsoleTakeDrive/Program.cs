using ConsoleTakeDrive.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = new ConfigurationBuilder();
BuildConfig(builder);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Logger.Information("App starting");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<AppConfig>();
        services.AddTransient<IApp, App>();

    })
    .UseSerilog()
    .Build();

var svc = ActivatorUtilities.CreateInstance<App>(host.Services);
svc.Run(args);

static void BuildConfig(IConfigurationBuilder builder)
{
    builder
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        ;
}