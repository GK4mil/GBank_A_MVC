using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Infrastructure.Repositories.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GBankAdminService.Infrastructure.Persistence
{
    public static class PersistenceWithEFRegistration
    {
        public static IServiceCollection AddGBankPersistenceEFServices(this IServiceCollection services, IConfiguration configuration)
        {
            IServiceCollection serviceCollections = services.AddDbContext<GBankDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSQLConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IAdminRepository,AdminRepository>();
            services.AddScoped<IAdminPrivilegeRepository, AdminPrivilegeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            //services.AddScoped<IBillRepository, BillRepository>();

            //services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
