using ECOM.CORE.Interfaces;
using ECOM.INFRASTRUCTURE.Data;
using ECOM.INFRASTRUCTURE.Repositories;
using ECOM.INFRASTRUCTURE.Repositries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECOM.INFRASTRUCTURE
{
    public static class InfrastructureRegisteration
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //apply Unit Of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //apply DbContext
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcommerceDB"));
            }); 
            return services;
        }
    }
}
