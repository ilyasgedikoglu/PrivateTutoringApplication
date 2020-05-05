using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Model.Entity;

namespace PrivateTutoringApplication.Shared.DTO
{
    [DataContract]
    [Serializable]
    public class GirisDTO : BaseModelDTO
    {
        [DataMember]
        public int KullaniciId { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public bool Durum { get; set; }

        [DataMember]
        public List<IPAddress> IPAddress { get; set; }

        [DataMember]
        public virtual Kullanici Kullanici { get; set; }
    }
}
