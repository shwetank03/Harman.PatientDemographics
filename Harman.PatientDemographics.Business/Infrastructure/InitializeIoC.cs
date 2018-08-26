using Harman.PatientDemographics.Dal;
using Harman.PatientDemographics.Dal.Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Harman.PatientDemographics.Business.Contract;
using Harman.PatientDemographics.Repository;
using Harman.PatientDemographics.Repository.Contract;

namespace Harman.PatientDemographics.Business.Infrastructure
{
    /// <summary>
    /// This is a container class used for DI
    /// </summary>
    public static class InitializeIoC
    {
        /// <summary>
        /// This methiods register all the dependencies required for the application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionString"></param>
        public static void RegisterAll(IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IAdoDataContext>(ctx => new AdoDataContext(connectionString));
            services.AddSingleton<IPatientRepository, PatientRepository>();
            services.AddSingleton<IPatientProvider, PatientProvider>();
        }
    }
}
