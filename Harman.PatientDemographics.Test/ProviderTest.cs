using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Harman.PatientDemographics.Business.Contract;
using Xunit;
using Harman.PatientDemographics.ViewModel;
using Moq;

namespace Harman.PatientDemographics.Test
{
    /// <summary>
    /// This test class provides test results for business logic for adding patient records in database.
    /// </summary>
    public class ProviderTest
    {
        private readonly Mock<IPatientProvider> _patientMockProvider;

        public ProviderTest()
        {
            _patientMockProvider = new Mock<IPatientProvider>();
        }
        /// <summary>
        /// This test methods adds the patient record in Database.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task ProviderTestForPatientAdd()
        {
            var patientTestData = GeneratePatientMockData();

            var apiBaseResponseMockData = GenerateApiBaseResponseMockData();

            _patientMockProvider.Setup(provider => provider.AddPatientAsync(patientTestData)).ReturnsAsync(apiBaseResponseMockData);

            var result = await _patientMockProvider.Object.AddPatientAsync(patientTestData);

            Assert.Equal(result, apiBaseResponseMockData);
        }

        private static PatientDetailViewModel GeneratePatientMockData()
        {
            return new PatientDetailViewModel
            {
                Gender = Gender.Male,
                DateOfBirth = Convert.ToDateTime("12/09/1987"),
                Forename = "Shwetank",
                Surname = "Pandey",
                TelephoneNumbers = new Dictionary<ContactType, ICollection<string>>
                {
                    {
                        ContactType.MobileNumber, new List<string>
                        {
                            "9980915657"
                        }
                    }
                }
            };
        }

        private static ApiBaseResponse GenerateApiBaseResponseMockData()
        {
            return new ApiBaseResponse
            {
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
