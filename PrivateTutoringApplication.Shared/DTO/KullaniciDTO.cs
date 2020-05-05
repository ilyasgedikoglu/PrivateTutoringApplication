using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PrivateTutoringApplication.Model.Entity;

namespace PrivateTutoringApplication.Shared.DTO
{
    [DataContract]
    [Serializable]
    public class KullaniciDTO : BaseModelDTO
    {
        [DataMember]
        public string Ad { get; set; }

        [DataMember]
        public string Soyad { get; set; }

        [DataMember]
        public string KullaniciAdi { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string TelephoneNumber { get; set; }

        [DataMember]
        public string Sifre { get; set; }

        [DataMember]
        public string TuzlamaDegeri { get; set; }

        [DataMember]
        public string KimlikNo { get; set; }

        [DataMember]
        public string Resim { get; set; }

        [DataMember]
        public string DogumYeri { get; set; }

        [DataMember]
        public DateTime? DogumTarihi { get; set; }

        [DataMember]
        public int? YetkiId { get; set; }

        [DataMember]
        public string Aciklama { get; set; }

        [DataMember]
        public string Biyografi { get; set; }

        [DataMember]
        public string CVPath { get; set; }

        [DataMember]
        public string School { get; set; }

        [DataMember]
        public string UpperSchool { get; set; }

        [DataMember]
        public string Department { get; set; }

        [DataMember]
        public string UpperDepartment { get; set; }

        [DataMember]
        public string Job { get; set; }

        [DataMember]
        public string GraduationStatus { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public virtual Yetki Yetki { get; set; }
    }
}
