using FinalProjectCMS.Models;
using FinalProjectCMS.Repository.Admin;
using FinalProjectCMS.Repository.Doctor;
using FinalProjectCMS.Repository.LabTechnician;
using FinalProjectCMS.Repository.Pharmacist;
using FinalProjectCMS.Repository.Receptionist;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FinalProjectCMS
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
            //ConnectionString for DB, inject as dependency
            services.AddDbContext<ASPCMSDBContext>(db => db.UseSqlServer(Configuration.GetConnectionString("cmsConnection")));
            
            
            //Doctor//
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IDiagnosisRepository, DiagnosisRepository>();
            services.AddScoped<IPatientDetailsRepository, PatientDetailsRepository>();
            services.AddScoped<IPatientHistoryRepository, PatientHistoryRepository>();

            services.AddLogging();

            //Receptionist
            services.AddScoped<Repository.Receptionist.IPatientRepository, Repository.Receptionist.PatientRepository>();
            
            services.AddScoped<IAppointmentsRepository, AppointmentsRepository>();





            //Admin

            services.AddScoped<ILabRepository, LabRepository>();
            services.AddScoped<Repository.Admin.IMedicineRepository, Repository.Admin.MedicineRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<IUserLoginRepository, UserLoginRepository>();


            //Lab Technicians
            services.AddScoped<ILabTestList, LabTestList>();
            services.AddScoped<ILabReportRepository, LabReportRepository>();





            //Pharmacist
            //add dependency injection of MedicineRepository
            services.AddScoped<Repository.Pharmacist.IMedicineRepository, Repository.Pharmacist.MedicineRepository>();
            services.AddScoped<Repository.Pharmacist.IPatientRepository, Repository.Pharmacist.PatientRepository>();
            services.AddScoped<IPatientMedRepository, PatientMedRepository>();


            //Json Resolver

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });


            //Enable Cors
            services.AddCors();


            //Add swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Clinic Management System", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //need to add below two lines
           app.UseCors(options =>
           options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Enable Swagger UI only in the development environment
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic Management API Vi"));
            }

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
