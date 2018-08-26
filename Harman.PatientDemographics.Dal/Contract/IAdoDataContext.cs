using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Harman.PatientDemographics.Dal.Contract
{
   public interface IAdoDataContext
    {
        void Dispose();
        Task<Dal> GetDbAsync();
    }
}
