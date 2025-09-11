using Accounting.Core.Repositories;
using Accounting.Core.Services;
using Accounting.Core.UnitOfWorks;
using Accounting.Repository.Context;
using Accounting.Repository.Repositories;
using Accounting.Repository.UnitOfWorks;
using Accounting.Service.Mappings;
using Accounting.Service.Services;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;
namespace Accounting.API.Modules
{
    public class RepoServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWorks>().As<IUnitOfWorks>();

            var apiAssembly = Assembly.GetExecutingAssembly();
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapperProfile));

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
               .Where(x => x.Name.EndsWith("Service"))
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

        }
    }
}
