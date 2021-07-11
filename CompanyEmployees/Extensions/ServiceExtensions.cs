
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

public static class ServiceExtensions{
    public static void ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
    }
    public static void ConfigureIISIntergation(this IServiceCollection services)
    {
        services.Configure<IISOptions>(options =>{});
    }
    public static void ConfigureLoggerService(this IServiceCollection services)
    {
        services.AddScoped<ILoggerManager, LoggerManager>();
    }
    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) {
        services.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),b=> {
                b.MigrationsAssembly("CompanyEmployees");
            });
        });}
     
    public static void ConfigureRepositoryManager(this IServiceCollection services)
    {
        services.AddScoped<RepositoryManager, RepositoryManager>();
    }
   
    }

