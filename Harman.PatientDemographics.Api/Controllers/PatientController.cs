using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Harman.PatientDemographics.Business.Contract;
using Harman.PatientDemographics.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Harman.PatientDemographics.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Patient")]
    public class PatientController : Controller
    {
        private readonly IPatientProvider _patientProvider;
        public PatientController(IPatientProvider patientProvider)
        {
            _patientProvider = patientProvider;
        }
        /// <summary>
        /// This Api does the following operation
        /// 1. Checks for valid Model State.
        /// 2. If Valid then call the provider for storing the data in database.
        /// </summary>
        /// <param name="patientDetailViewModel"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public async Task<ObjectResult> AddPatient([FromBody]PatientDetailViewModel patientDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                return CreateResponse(await _patientProvider.AddPatientAsync(patientDetailViewModel));
            }
            else
            {
                return CreateResponse(new ApiBaseResponse
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Error = "Data passed is invalid"
                });
            }
        }

        /// <summary>
        /// This API returns the List of patients from Database.
        /// </summary>
        /// <returns></returns>
        public async Task<ObjectResult> GetPatient()
        {
            return CreateResponse(await _patientProvider.GetPatients());
        }

        /// <summary>
        /// This method provides the API Base Response.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected ObjectResult CreateResponse(ApiBaseResponse result)
        {
            return new ObjectResult(result) { StatusCode = (int)(result.StatusCode) };
        }
    }
}