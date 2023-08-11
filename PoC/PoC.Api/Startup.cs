using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PoC.BL;
using PoC.BL.AbstractProducts;
using PoC.BL.ConcreteProducts;
using PoC.Data.AbstractProducts.UnitOfWork;
using PoC.Data.ConcreteProducts.UnitOfWork;
using PoC.Data.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoC.Api
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
            services.AddDbContext<PoCDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PoCDbContext"));
            }, ServiceLifetime.Transient);

            services.AddUnitOfWork<PoCDbContext, IPoCUnitOfWork, PoCUnitOfWork>(repoConfig =>
                repoConfig.AddRepositoriesFromAssemblyOf<PoCUnitOfWork>());
            services.AddTransient<IJobService, JobService>();

            services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);
            services.AddControllers();
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

            app.UseCors(cors => cors
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .WithOrigins("https://localhost:44319/")
                      .SetIsOriginAllowed(origin => true)
                      .AllowCredentials()
                     );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
