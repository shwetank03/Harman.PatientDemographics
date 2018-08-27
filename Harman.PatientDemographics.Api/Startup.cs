using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Harman.PatientDemographics.Business.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace Harman.PatientDemographics.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        private string ConnectionString => Configuration.GetConnectionString("_PatientDemographicsDataContext");

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddWebApi(services);
            InitializeIoC.RegisterAll(services, ConnectionString);
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();

                    policyBuilder.WithOrigins("http://localhost:4200");
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseCors("AllowAll");
        }

        public static IMvcCoreBuilder AddWebApi(IServiceCollection services)
        {
            var builder = services.AddMvcCore(c =>
            {

            });
            builder.AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });
            builder.AddJsonFormatters(f => f.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);
            return builder;
        }
    }
}
