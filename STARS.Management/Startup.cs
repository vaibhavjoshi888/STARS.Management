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
        services.Configure<LDAPContext>(configRoot.GetSection("LDAPContext"));
        services.AddSingleton<DapperContext>();
        services.AddSwaggerGen();
        services.AddControllers();
        services.AddMvc();
        services.AddSingleton<ILDAPService, LDAPService>();
        services.AddScoped<IUserManagementService, UserManagementService>();
        services.AddScoped<IUserManagementRepository, UserManagementRepository>();
        services.AddEndpointsApiExplorer();
        services.AddOptions();
        

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
        app.Run();
    }
}
