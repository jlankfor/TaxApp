using MvvmCross.IoC;
using MvvmCross.ViewModels;
using JuniperTaxApp.Core.ViewModels.Home;
using JuniperTaxApp.Core.Container;
using System.Collections.Generic;
using JuniperTaxApp.Core.ViewModels;

namespace JuniperTaxApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            var containerModule = new List<ContainerModule>()
            {
                new ContainerModule()
            };
            DependencyContainer.Build(containerModule);

            //var n = IMvxIoCProvider
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            CreatableTypes()
                .EndingWith("Client")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<OrderDetailsViewModel>();
        }
    }
}
