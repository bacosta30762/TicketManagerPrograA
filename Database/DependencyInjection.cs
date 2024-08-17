using Database.Interfaces;
using Database.Models;
using Database.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjection
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurar el contexto de la base de datos
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Configurar los servicios de identidad
            services.AddIdentity<User, Role>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Repositorios
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IHistoryRepository, HistoryRepository>();
        }
    }
}
