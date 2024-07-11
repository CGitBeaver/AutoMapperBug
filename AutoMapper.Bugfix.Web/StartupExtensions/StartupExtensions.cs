using AutoMapper.Bugfix.Web.MappingProfiles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AutoMapper.Bugfix.Web.StartupExtensions;

public static class StartupExtensions
{
    public static void ConfigureService(this IApplicationBuilder app, IWebHostEnvironment env, IMapper autoMapper)
    {
        _ = autoMapper ?? throw new ArgumentNullException(nameof(autoMapper));

        app.UseStaticFiles();

        if (env.IsDevelopment())
        {
        }
        app.UseExceptionHandler("/error");

        app.UseResponseCaching();

        app.Use(async (context, next) =>
        {
            context.Response.GetTypedHeaders().CacheControl =
                new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                {
                    NoStore = true
                };
            await next().ConfigureAwait(false);
        });

        app.UseRouting();

        app.UseAuthorization();

        app.UseMvc();
    }

    public static void AddService(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddResponseCaching();

        services.AddHttpContextAccessor();

        services.AddAutoMapper(
            typeof(AutoMappingWeb));

        services.AddMvc(option =>
        {
            option.EnableEndpointRouting = false;

        });
    }
}
