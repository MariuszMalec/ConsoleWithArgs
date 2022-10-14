using ConsoleTakeDrive.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class App : IApp
{
    private readonly ILogger<App> _log;
    private readonly IConfiguration _config;
    private readonly AppConfig _appConfig;
    public App(ILogger<App> log, IConfiguration config, AppConfig appConfig = null)
    {
        _log = log;
        _config = config;
        _appConfig = appConfig;
    }

    public void Run(string[] args)
    {
        if (args.Length == 0)
        {
            _log.LogWarning("Invalid args");
            return;
        }

        var command = args[0];

        switch (command)
        {
            case "-S":
                CopyS();
                break;
            case "-O":
                CopyO();
                break;
            case "-SD" when args.Length == 3 && args[1] == "-d":
                CopySPP(args[2]);
                break;
            default:
                _log.LogWarning("Invalid command");
                break;
        }
    }

    void CopySPP(string message)
    {
        _log.LogInformation($"Executing copy to S with : {message}");

        var apiKey = _config.GetSection("ServiceConfiguration:ApiKey").Value;

        _config.GetSection(AppConfig.SectionName).Bind(_appConfig, c => c.BindNonPublicProperties = true);

        _log.LogInformation($"Apikey: {apiKey}");
        _log.LogInformation($"ContactEmail: {_appConfig.ContactEmail}");
    }
    void CopyS()
    {
        _log.LogInformation("Executing copy to S drive");
    }
    void CopyO()
    {
        _log.LogInformation($"Executing copy to one drive");
    }

}
