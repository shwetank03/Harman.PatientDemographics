using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Harman.PatientDemographics.ViewModel
{
    public class PatientDetailViewModel
    {
        public PatientDetailViewModel()
        {
            TelephoneNumbers = new Dictionary<ContactType, ICollection<string>>();
        }
        [Required, MinLength(3), MaxLength(50)]
        public string Forename { get; set; }
        [Required, MinLength(2), MaxLength(50)]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public IDictionary<ContactType, ICollection<string>> TelephoneNumbers { get; set; }
    }
}
