using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RecruitmentManagementApi.Mappings;
using RecruitmentManagementApi.Models.Entities;
using RecruitmentManagementApi.Repositories;
using RecruitmentManagementApi.Services;

namespace RecruitmentManagementApi
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

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(
                        "v1",
                        new OpenApiInfo
                        {
                            Title = "Swagger Recruitment Management API", Version = "v1",
                        }
                    );
                }
            );

            var mappingConfig = new MapperConfiguration(
                mc => mc.AddProfile(new ProfileMapper())
            );

            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddDbContext<RecruitmentManagementContext>(
                x =>
                    x.UseSqlServer(
                        Configuration.GetConnectionString("RecruitmentManagementConnection")
                    )
            );

            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllers();
                }
            );

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger XML Api Demo v1"));
        }

        private static void RegisterServices(IServiceCollection services)
        {
            var names = new List<string>
            {
                "Service", "Repository",
            };

            services.Scan(
                scan =>
                    scan.FromAssemblies(
                            typeof(StatusService).Assembly,
                            typeof(StatusRepository).Assembly
                        )
                        .AddClasses(
                            x => x.Where(
                                c => names.Any(
                                    name => c.Name.EndsWith(name)
                                )
                            )
                        )
                        .AsMatchingInterface()
            );
        }
    }
}