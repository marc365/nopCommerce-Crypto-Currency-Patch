using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Orders;
using Nop.Web.Factories;

namespace Nop.Plugin.Misc.CoinService
{
    public class SourceCodeDependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<BitcoinPriceFormatter>().As<IPriceFormatter>().InstancePerLifetimeScope();
            builder.RegisterType<BitcoinCurrencyService>().As<ICurrencyService>().InstancePerLifetimeScope();
            builder.RegisterType<BitcoinOrderProcessingService>().As<IOrderProcessingService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 1; } //Overrides
        }

    }
}
