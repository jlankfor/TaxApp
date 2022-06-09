using System;
using System.Collections.Generic;
using JuniperTaxApp.Core.Container;
using Ninject;

namespace JuniperTaxApp.Core.Container
{
    // might have to subclass MvxSingleton
    public static class DependencyContainer
    {
        static IKernel _kernal;

        public static void Build(List<ContainerModule> containerModule)
        {
            var settings = new NinjectSettings
            {
                InjectNonPublic = true,
                InjectParentPrivateProperties = true
            };

            Build(new StandardKernel(settings, containerModule.ToArray()));
        }

        public static void Build(IKernel kernal)
        {
            _kernal = kernal;
        }
    }
}
