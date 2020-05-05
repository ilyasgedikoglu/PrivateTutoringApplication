using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Shared.CriteriaObject
{
    [DataContract]
    [Serializable]
    public class HomeIndexCO
    {
        [DataMember]
        public IEnumerable<KullaniciDTO> Kullanici { get; set; }

        [DataMember]
        public IEnumerable<LessonDTO> Lesson { get; set; }
    }
}
