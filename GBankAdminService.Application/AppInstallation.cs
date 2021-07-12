using Microsoft.Extensions.DependencyInjection;

namespace GBankAdminService.Application
{
    public static class AppInstallation
    {
        public static IServiceCollection AppGBankApplication(this IServiceCollection services)
        {
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // services.AddMediatR(typeof(GBank.Application.Functions.Authentication.Command.LoginCommand).GetTypeInfo().Assembly);

            return services;
        }

    }
}
