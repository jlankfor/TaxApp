using MvvmCross.IoC;
using MvvmCross.ViewModels;
using JuniperTaxApp.Core.ViewModels.Home;

namespace JuniperTaxApp.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<HomeViewModel>();
        }
    }
}
