using AutoMapper.Bugfix.Web.StartupExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace AutoMapper.Bugfix.Starter;


public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddService(Configuration);

        services.AddAuthentication(IISDefaults.AuthenticationScheme);

        services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
 
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            c.UseAllOfToExtendReferenceSchemas();

            c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Version = "v1",
                Title = $"AutoMapping Bug",
                Description = "AutoMapper"
            });

            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            foreach (var fi in dir.EnumerateFiles("AutoMapper.Bugfix.*.xml"))
            {
                c.IncludeXmlComments(fi.FullName);
            }

            c.MapType<FileStreamResult>(() => new OpenApiSchema
            {
                Type = "file",
            });
        });

        services.Configure<IISOptions>(opts =>
        {
            opts.AutomaticAuthentication = false;
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper autoMapper)
    {
        _ = app ?? throw new ArgumentNullException(nameof(app));

        var supportedCultures = new[] { "de-DE", "en-US" };

        var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        app.ConfigureService(env, autoMapper);

        app.UseStaticFiles();

        var config = (IConfiguration?)app.ApplicationServices.GetService(typeof(IConfiguration));

        if (config is null)
        {
            throw new Exception("Could not load Configuration from AppSettings.json");
        }
        else
        {
            bool enableSwagger = bool.Parse(config.GetValue<string>("EnableSwagger") ?? "false");

            if (enableSwagger)
            {
                app.UseSwagger();
                
                
                string subDir = config.GetValue<string>("IisSubDirPath") ?? "";

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"{subDir}/swagger/v1/swagger.json", "AM API V1");
                });
            } 
        }
    }
}
