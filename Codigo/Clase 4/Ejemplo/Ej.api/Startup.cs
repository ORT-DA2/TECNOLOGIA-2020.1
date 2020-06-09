using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Ej.Domain;
using Ej.BL.Interface;
using Ej.DA.Interface;
using Ej.DA;
using Ej.BL;
using Swashbuckle.AspNetCore;

namespace Ej.api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Api de ejemplo", Version = "v1"});
            });

            services.AddDbContext<DbContext, EjContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString("EjDB"))
            );
            services.AddScoped<IManagerDA<Person>, PersonManagerDA>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IManagerDA<Homework>, HomeworkManagerDA>();
            services.AddScoped<IHomeworkService, HomeworkService>();

            services.AddCors(
                options => { options.AddPolicy(
                    "CorsPolicy", 
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                );
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api ejemplo v1");
            });

             app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();           

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
