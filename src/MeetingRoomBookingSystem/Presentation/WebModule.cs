using Autofac;
using DataAccess.Data;
using DataAccess.Identity;
using DataAccess.Repositories;
using DataAccess.UnitOfWorks;
using Domain.RepositoryContracts;
using Services;
using Services.Implementations;
using Services.Interfaces;

namespace Presentation
{
    public class WebModule(string connectionString, string migrationAssembly) : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>().AsSelf()
                .WithParameter("connectionString", connectionString)
                .WithParameter("migrationAssembly", migrationAssembly)
                .InstancePerLifetimeScope();

            builder.RegisterType<MeetingRoomUnitOfWork>()
.As<IMeetingRoomUnitOfWork>()
.InstancePerLifetimeScope();

            builder.RegisterType<UserService>()
                .As<IUserService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<FileUploadService>()
                .As<IFileUploadService>()
                .InstancePerLifetimeScope();



            //Meeting
            builder.RegisterType<MeetingRoomRepository>()
                .As<IMeetingRoomRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<MeetingRoomManagementService>()
                .As<IMeetingRoomManagementService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingRepository>()
    .As<IBookingRepository>()
    .InstancePerLifetimeScope();
        }
    }
}
