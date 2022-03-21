using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PMS.DAL;
using PMS.DAL.Repository;
using PMS.DAL.UnitOfWork;
using PMS.Domain.DTO;
using PMS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.WebAPI
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
            //services.AddDbContext<ApplicationDBContext>(options =>
            //            options.UseSqlServer(Configuration.GetConnectionString("APIConnection"),
            //            b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));

            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var user = Configuration["DBUser"] ?? "SA";
            var password = Configuration["DBPassword'"] ?? "Pass33w0rd2022";
            var database = Configuration["Database"] ?? "Batch9PMSDB";

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IAllergyRepository, AllergyRepository>();
            services.AddTransient<IDiagnosisRepository, DiagnosisRepository>();
            services.AddTransient<IDrugDataRepository, DrugDataRepository>();
            services.AddTransient<IMailRepository, MailRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDBContext>(options =>
                  options.UseSqlServer($"server={server},port={port};Database={database};User ID={user};Password={password}"));

            services.AddCors();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            PrepDB.PrepPopution(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin()
                                         .AllowAnyMethod()
                                         .AllowAnyHeader());

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
