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

        public static T Get<T>(string name = null)
        {
            if(_kernal == null)
            {
                throw new NullReferenceException("IKernal container is null");
            }

            return _kernal.Get<T>(name);
        }

        public static bool TryGet<T>(out T parameter, string name = null)
        {
            parameter = default;

            if (_kernal == null)
            {
                throw new NullReferenceException("IKernal container is null");
            }

            parameter = _kernal.Get<T>(name);
            return true;
        }

        public static void Inject<T>(object obj)
        {
            if (_kernal == null)
            {
                throw new NullReferenceException("IKernal container is null");
            }

            _kernal.Inject(obj);
        }

        public static void BindInstance<T>(T instance)
        {
            if (_kernal == null)
            {
                throw new NullReferenceException("IKernal container is null");
            }

            _kernal.Bind<T>().ToConstant(instance);
        }
    }
}
