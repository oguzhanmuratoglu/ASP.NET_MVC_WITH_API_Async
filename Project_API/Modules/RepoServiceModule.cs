using Autofac;
using Project_Core.Repositories;
using Project_Core.Services;
using Project_Core.UnitOfWork;
using Project_Repository.Repositories;
using Project_Repository.UnitOfWorks;
using Project_Service.Mapping;
using Project_Service.Services;
using System.Reflection;
using System;
using Module = Autofac.Module;
using Project_Repository;

namespace Project_API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();


            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(ProjectDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));


            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Repository")).
                AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).
                AsImplementedInterfaces().InstancePerLifetimeScope();

            //InstancePerLifetimeScope => scope;
            //InstancePerDependency => transient;
        }
    }
}
