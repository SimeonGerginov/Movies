[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Movies.Web.App_Start.DependencyInjectionConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Movies.Web.App_Start.DependencyInjectionConfig), "Stop")]

namespace Movies.Web.App_Start
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using AutoMapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Movies.Core.Contracts;
    using Movies.Infrastructure.Attributes;
    using Movies.Infrastructure.Filters;
    using Movies.Persistence.Data;
    using Movies.Persistence.Data.Repositories;
    using Movies.Persistence.Data.UnitOfWork;
    using Movies.Services.Contracts;

    using Ninject;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.Common;
    using Ninject.Web.Mvc.FilterBindingSyntax;

    public static class DependencyInjectionConfig 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            kernel.Bind(x =>
            {
                x.FromAssemblyContaining(typeof(IDataService))
                .SelectAllClasses()
                .BindDefaultInterface();
            });

            kernel
                .Bind(typeof(DbContext), typeof(MsSqlDbContext))
                .To<MsSqlDbContext>()
                .InRequestScope();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(EfRepository<>)).InRequestScope();
            kernel.Bind<IMapper>().ToMethod(ctx => Mapper.Instance).InSingletonScope();

            kernel.BindFilter<SaveChangesFilter>(System.Web.Mvc.FilterScope.Controller, 0)
                .WhenActionMethodHas<SaveChangesAttribute>();
        }        
    }
}
