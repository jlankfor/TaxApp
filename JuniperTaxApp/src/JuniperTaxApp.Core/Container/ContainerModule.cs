using System;
using JuniperTaxApp.Core.Clients;
using JuniperTaxApp.Core.Interfaces;
using Ninject.Modules;

namespace JuniperTaxApp.Core.Container
{
    public class ContainerModule : NinjectModule
    {
        public ContainerModule()
        {
        }

        public override void Load()
        {
            Bind<ITaxClient>().To<TaxClient>().InSingletonScope();
        }
    }
}
