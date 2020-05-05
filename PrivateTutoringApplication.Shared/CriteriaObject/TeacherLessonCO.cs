using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Shared.CriteriaObject
{
    [DataContract]
    [Serializable]
    public class TeacherLessonCO
    {
        [DataMember]
        public KullaniciDTO Kullanici { get; set; }

        [DataMember]
        public IEnumerable<TutorLessonDTO> TutorLesson { get; set; }

    }
}
