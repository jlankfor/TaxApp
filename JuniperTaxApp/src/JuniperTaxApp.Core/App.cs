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

            //var n = IMvxIoCProvider
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            CreatableTypes()
                .EndingWith("Client")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            //Mvx.IoCProvider.RegisterType<IUserDialogs, UserDialogs.Instance>();
            //Mvx.IoCProvider.RegisterType<ITaxService, TaxService>();
            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);

            RegisterAppStart<OrderDetailsViewModel>();
        }
    }
}
