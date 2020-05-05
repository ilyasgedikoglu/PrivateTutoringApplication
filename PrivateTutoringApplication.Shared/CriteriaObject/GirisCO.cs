using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace PrivateTutoringApplication.Shared.CriteriaObject
{
    [DataContract]
    [Serializable]
    public class GirisCO
    {
        [DataMember]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DataMember]
        public string Sifre { get; set; }

    }
}
