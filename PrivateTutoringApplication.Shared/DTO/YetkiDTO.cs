using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PrivateTutoringApplication.Shared.DTO
{
    [DataContract]
    [Serializable]
    public class YetkiDTO : BaseModelDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Adi { get; set; }

        [DataMember]
        public string Aciklama { get; set; }

        [DataMember]
        public bool Goster { get; set; }
    }
}
