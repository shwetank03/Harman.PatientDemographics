using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harman.PatientDemographics.Dal;
using Harman.PatientDemographics.Dal.Contract;
using Harman.PatientDemographics.Domain;
using Harman.PatientDemographics.Repository.Contract;
using Harman.PatientDemographics.Repository.DbContract;
namespace Harman.PatientDemographics.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IAdoDataContext _adoDataContext;
        public PatientRepository(IAdoDataContext adoDataContext)
        {
            _adoDataContext = adoDataContext;
        }

        public async Task<int> AddPatientAsync(PatientDetail patientDetail)
        {
            using (IDal dal = await _adoDataContext.GetDbAsync())
            {
                IDataParameter[] parameters =
                {
                        await  dal.GetParameterAsync("@Record", patientDetail.Record)
                    };
                return await dal.ExecuteScalarAsync<int>(Constants.PatientDetailsAdd, parameters,
                    CommandType.StoredProcedure);
            }
        }

        public async Task<ICollection<PatientDetail>> GetPatients()
        {
            using (IDal dal = await _adoDataContext.GetDbAsync())
            {
                using (var dr = await dal.SelectAsync(Constants.PatientDetailsGet))
                {
                    var relations = await dr.ToListAsync(PatientDetailMapper);
                    return relations.ToList();
                }
            }
        }

        private static PatientDetail PatientDetailMapper(IDataReader dr)
        {
            return new PatientDetail
            {
                Id = dr.GetDrValue<int>("Id"),
                Record = dr.GetDrValue<string>("Record"),
            };
        }
    }
}
