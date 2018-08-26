using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harman.PatientDemographics.Business.Contract;
using Harman.PatientDemographics.Repository.Contract;
using Harman.PatientDemographics.ViewModel;
using Microsoft.Extensions.Logging;
using Harman.PatientDemographics.Common;
using Harman.PatientDemographics.Domain;

namespace Harman.PatientDemographics.Business
{
    /// <summary>
    /// This class provides Business logic implementation for storing and fetching the patient records from Database.
    /// </summary>
    public class PatientProvider : IPatientProvider
    {
        private readonly IPatientRepository _patientRepository;
        private readonly ILogger<PatientProvider> _logger;

        public PatientProvider(IPatientRepository patientRepository, ILogger<PatientProvider> logger)
        {
            _patientRepository = patientRepository;
            _logger = logger;
        }

        /// <summary>
        /// This method does the following operations :
        ///Takes the patient records, converts it to XML and calls the repository for saving the information.
        /// 
        /// </summary>
        /// <param name="patientDetailViewModel"></param>
        /// <returns></returns>
        public async Task<ApiBaseResponse> AddPatientAsync(PatientDetailViewModel patientDetailViewModel)
        {
            var response = new ApiBaseResponse();
            try
            {
                var patientDetail = new PatientDetail
                {
                    Record = patientDetailViewModel.SerializeObject()
                };
                await _patientRepository.AddPatientAsync(patientDetail);
                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = "Error Occured while saving patient information";
                _logger.LogError("Error Occured while saving patient information", ex);
            }
            return response;
        }

        /// <summary>
        /// This method does the following operations :
        /// Get the Patients lists from Database converts it to Object and returns it back to the API.
        /// </summary>
        /// <returns></returns>
        public async Task<PatientResponse> GetPatients()
        {
            var response = new PatientResponse();
            try
            {
                var domainPatients = await _patientRepository.GetPatients();

                domainPatients.ToList().ForEach(patient =>
                {
                    response.PatientDetailViewModels.Add(patient.Record.Deserialize<PatientDetailViewModel>());
                });

                response.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Error = "Error Occured while fetching patient information";
                _logger.LogError("Error Occured while fetching patient information", ex);
            }
            return response;
        }
    }
}
