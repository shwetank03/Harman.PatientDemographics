using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Harman.PatientDemographics.ViewModel
{
    public class PatientResponse : ApiBaseResponse
    {
        public PatientResponse()
        {
            PatientDetailViewModels = new Collection<PatientDetailViewModel>();
        }
        public ICollection<PatientDetailViewModel> PatientDetailViewModels { get; set; }
    }
}
