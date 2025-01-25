using Autofac;
using DevSkill.Inventory.Domain;
using DevSkill.Inventory.Infrastructure;
using DevSkill.Inventory.Infrastructure.Identity;
using DevSkill.Inventory.Web.Data;

namespace DevSkill.Inventory.Web
{
    public class WebModule(string connectionString, string migrationAssembly) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<InventoryDbContext>().AsSelf()
            .WithParameter("connectionString", connectionString)
            .WithParameter("migrationAssembly", migrationAssembly)
            .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", connectionString)
                .WithParameter("migrationAssembly", migrationAssembly)
                .InstancePerLifetimeScope();

            //builder.RegisterType<InventoryUnitOfWork>()
            //    .As<IInventoryUnitOfWork>()
            //    .InstancePerLifetimeScope();

            //EmailUtility
            builder.RegisterType<EmailUtility>()
                .As<IEmailUtility>()
                .InstancePerLifetimeScope();

            //UserService
            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

        }
    }
}
