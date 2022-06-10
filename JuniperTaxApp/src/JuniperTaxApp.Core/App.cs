using MvvmCross.IoC;
using MvvmCross.ViewModels;
using JuniperTaxApp.Core.ViewModels.Home;
using JuniperTaxApp.Core.Container;
using System.Collections.Generic;
using JuniperTaxApp.Core.ViewModels;
using MvvmCross;
using JuniperTaxApp.Core.Interfaces;
using Acr.UserDialogs;
using JuniperTaxApp.Core.Services;

namespace JuniperTaxApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            //var containerModule = new List<ContainerModule>()
            //{
            //    new ContainerModule()
            //};
            //DependencyContainer.Build(containerModule);

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
