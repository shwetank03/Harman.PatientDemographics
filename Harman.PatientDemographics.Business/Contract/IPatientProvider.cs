using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Harman.PatientDemographics.ViewModel;

namespace Harman.PatientDemographics.Business.Contract
{
    public interface IPatientProvider
    {
        Task<ApiBaseResponse> AddPatientAsync(PatientDetailViewModel patientDetailViewModel);

        Task<PatientResponse> GetPatients();
    }
}
