using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Model.Entity;

namespace PrivateTutoringApplication.Shared.CriteriaObject
{
    [DataContract]
    [Serializable]
    public class StudentRegisterCO
    {
        [DataMember]
        [Required]
        public string Ad { get; set; }

        [DataMember]
        [Required]
        public string Soyad { get; set; }

        [DataMember]
        [Required]
        public string KullaniciAdi { get; set; }

        [DataMember]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DataMember]
        public string Sifre { get; set; }

        [DataMember]
        [Required]
        public string TelephoneNumber { get; set; }
    }
}
