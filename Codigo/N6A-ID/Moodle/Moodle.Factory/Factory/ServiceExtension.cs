using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moodle.BusinessLogic;
using Moodle.BusinessLogic.Interface;
using Moodle.DataAccess;
using Moodle.DataAccess.Interface;
using Moodle.Domain;

namespace Moodle.Factory
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddStudentLogic(this IServiceCollection service)
        {
            service.AddScoped<IStudentLogic, StudentLogic>();
            return service;
        }

        public static IServiceCollection AddSessionLogic(this IServiceCollection service)
        {
            service.AddScoped<ISessionLogic, SessionLogic>();
            return service;
        }

        public static IServiceCollection AddStudentRepository(this IServiceCollection service)
        {
            service.AddScoped<IRepository<Student>, StudentRepository>();
            return service;
        }

        public static IServiceCollection AddMoodleContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DbContext, MoodleContext>(o => o.UseSqlServer(connectionString).UseLazyLoadingProxies());
            return services;
        }

        public static IServiceCollection EnableAllCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowEverything", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });
            return services;
        }
    }
}