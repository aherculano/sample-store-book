using Application;
using Infrastructure.Settings;

namespace API.Controllers;


public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        ConfigureSettings(services);
        services.AddControllers();
        services.AddRouting();
        services.AddSwaggerGen();
        services.ConfigureApplication();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private void ConfigureSettings(IServiceCollection services)
    {
        var sqlSettings = new SqlSettings();
        Configuration.GetSection(SqlSettings.SettingsSection).Bind(sqlSettings);
        services.AddSingleton(sqlSettings);
    }
}