using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Services;
using SimpleInjector;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;

namespace PaymentGateway.Api
{
    public class IocConfig
    {
        public static void IntegrateSimpleInjector(IServiceCollection services)
        {
            var container = ApplicationContext.Container;
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(container));

            services.AddSimpleInjector(container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
            });
        }

        public static void InitializeContainer()
        {
            var container = ApplicationContext.Container;
            container.Register<IBankService, FakeBankService>();
            container.Verify();
        }
    }
}
