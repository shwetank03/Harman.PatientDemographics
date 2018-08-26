using Harman.PatientDemographics.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Harman.PatientDemographics.Repository.Contract
{
    public interface IPatientRepository
    {
        Task<int> AddPatientAsync(PatientDetail patientDetail);

        Task<ICollection<PatientDetail>> GetPatients();
    }
}
