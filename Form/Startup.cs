using Autofac;
using Form.Business.Services;
using Form.Infrastructure.Context;
using Form.IoCCollection;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;
using Serilog.Events;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.IgnoreNullValues = true;
        });
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());
        services.AddMvc();
        services.AddHealthChecks();
        services.AddSwaggerGen();
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.BuildContext(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        string LOGGINGLEVEL = Environment.GetEnvironmentVariable("LOGGINGLEVEL");
        var level = LogEventLevel.Error;
        if (LogEventLevel.Information.ToString().Equals(LOGGINGLEVEL))
            level = LogEventLevel.Information;

        var levelSwitch = new LoggingLevelSwitch();
        levelSwitch.MinimumLevel = level;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.ControlledBy(levelSwitch)
            .MinimumLevel.Override("System", LogEventLevel.Error)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Error)
            .WriteTo.Console(level, @"[{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x
          .AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/check-status");
        });

        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
