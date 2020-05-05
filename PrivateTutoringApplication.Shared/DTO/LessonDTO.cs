using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PrivateTutoringApplication.Shared.DTO
{
    [DataContract]
    [Serializable]
    public class LessonDTO : BaseModelDTO
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public string Definition { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public int Discount { get; set; }

        [DataMember]
        public string Resim { get; set; }
    }
}
