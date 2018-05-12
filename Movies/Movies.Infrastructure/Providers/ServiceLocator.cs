using System.Web.Mvc;
using Bytes2you.Validation;
using Movies.Infrastructure.Providers.Contracts;
using Ninject;

namespace Movies.Infrastructure.Providers
{
    public class ServiceLocator : IServiceLocator
    {
        private readonly IKernel kernel;

        static ServiceLocator()
        {
            InstanceProvider = new ServiceLocator(DependencyResolver.Current.GetService<IKernel>());
        }

        private ServiceLocator(IKernel kernel)
        {
            Guard.WhenArgument(kernel, "Kernel").IsNull().Throw();

            this.kernel = kernel;
        }

        public static IServiceLocator InstanceProvider { get; set; }

        public T ProvideInstance<T>()
        {
            return this.kernel.Get<T>();
        }
    }
}
