using Microsoft.EntityFrameworkCore;
using PaymentGateway.EntityFramework;
using PaymentGateway.Services;
using PaymentGateway.Specs.Fakes;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using TechTalk.SpecFlow;

namespace PaymentGateway.Specs
{
    [Binding]
    public class TestHooks
    {
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var container = ApplicationContext.Container;

            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "PaymentGatewaySpecs")
                .Options;

            container.Register<DataContext>(() => new DataContext(options), Lifestyle.Scoped);
            container.RegisterInstance<IBankService>(new TestBankService());

            ApplicationContext.Container.Verify();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            AsyncScopedLifestyle.BeginScope(ApplicationContext.Container);
        }

        [BeforeStep]
        public void BeforeStep()
        {
        }

        [AfterStep]
        public void AfterStep()
        {
            ApplicationContext.Resolve<DataContext>().SaveChanges();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            ApplicationContext.Resolve<DataContext>().Database.EnsureDeleted();
            Lifestyle.Scoped.GetCurrentScope(ApplicationContext.Container)?.Dispose();
        }
    }
}
