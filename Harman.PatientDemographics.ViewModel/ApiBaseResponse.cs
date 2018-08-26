using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Harman.PatientDemographics.ViewModel
{
    public class ApiBaseResponse
    {
        public string Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
