using System.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STARS.Management.Core.Interface;
using STARS.Management.Core.Models;
using STARS.Management.Core.Repository;
using STARS.Management.Core.Services;
using STARS.Management.Infrastructure.Context;
using STARS.Management.Infrastructure.StarManagement;
using STARS.Management.Infrastructure.UserManagement;
using STARS.Management.Infrastructure.Utility;

namespace STARS.Management;
public class Startup
{
    public IConfiguration configRoot
    {
        get;
    }

    public Startup(IConfiguration configuration)
    {
        configRoot = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddSwaggerGen();
        services.AddControllers();
        services.AddMvc();
        services.AddEndpointsApiExplorer();
        services.AddOptions();
        services.Configure<LDAPContext>(configRoot.GetSection("LDAPContext"));
        services.AddSingleton<DapperContext>();
        services.AddSingleton<ILDAPService, LDAPService>();
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<IUserManagementRepository, UserManagementRepository>();
        services.AddScoped<IStarManagementService, StarManagementService>();

        services.AddScoped<IStarManagementRepository, StarManagementRepository>();

        services.AddScoped<IEmailSettings, EmailSettings>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddSingleton<IQueryProviderService, QueryProviderService>();
        services.AddCors(options => { options.AddPolicy(name: "AllowOrigin", builder => { builder.WithOrigins("https://localhost:44351", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod(); }); });
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
        app.Run();
    }
}
