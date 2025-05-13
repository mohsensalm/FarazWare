using FarazWare.Application.Contracts;
using FarazWare.Persistence.Context;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace FarazWare.Persistence;

public static class DependencyInjectionExtensions
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Retrieve the connection string from the appsettings.json file
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // Registering DataContext as a scoped service
        services.AddDbContext<DataContext>(options =>
        {
            //options.UseSqlServer(connectionString);
        });

        // Registering IDbConnection and DataContext as scoped services
        services.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));

        // Registering DataContext as a scoped service
        services.AddScoped<DbContext, DataContext>();

        // Registering IUnitOfWork and UnitOfWork as scoped services
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}