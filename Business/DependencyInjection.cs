using Business.Interfaces;
using Business.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Business
{
    public static class DependencyInjection
    {
        public static void AddBusiness(this IServiceCollection services, IConfiguration configuration)
        {
            // Servicios
            services.AddTransient<ITicketService, TicketService>();
        }
    }
}

