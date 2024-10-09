using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Abstractions.Clock;
using CleanArchitecture.Infrastructure.Clock;
using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Domain.Vehiculos;
using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.Data;
using Dapper;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Email;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
      this IServiceCollection services, 
      IConfiguration configuration
    )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IEmailService, EmailService>();

        var connectionString = configuration.GetConnectionString("ConnectionString")
        ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAlquilerRepository, AlquilerRepository>();
        services.AddScoped<IVehiculoRepository, VehiculoRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>( _ => {
            return new SqlConnectionFactory(connectionString);
        });

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        return services;
    }
}