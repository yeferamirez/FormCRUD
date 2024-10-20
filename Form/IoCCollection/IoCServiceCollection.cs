
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Form.Business.Services;
using Form.Infrastructure.Context;
using Form.Infrastructure.Interfaces.Repositories;
using Form.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Form.IoCCollection
{
    public static class IoCServiceCollection
    {
        public static ContainerBuilder BuildContext(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);
            return BuildContext(builder, configuration);
        }

        public static ContainerBuilder BuildContext(this ContainerBuilder builder, IConfiguration configuration)
        {
            RegisterContext(builder, configuration);
            RegisterRepositories(builder);
            RegisterServices(builder);
            return builder;
        }

        private static void RegisterContext(ContainerBuilder builder, IConfiguration configuration)
        {
            builder
            .Register((context, parameters) =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Development"));
                var dbContext = new UserContext(optionsBuilder.Options);
                dbContext.Database.EnsureCreated();
                return dbContext;
            })
            .As<UserContext>()
            .InstancePerLifetimeScope();
        }

        private static void RegisterRepositories(ContainerBuilder builder)
        {
            builder
               .Register((context, parameters) => new UserRepository(
                   context.Resolve<UserContext>()
                   ))
               .As<IUserRepository>()
               .SingleInstance();
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder
               .Register((context, parameters) => new UserService(
                   context.Resolve<IUserRepository>()
                   ))
               .As<UserService>()
               .SingleInstance();
        }
    }
}
