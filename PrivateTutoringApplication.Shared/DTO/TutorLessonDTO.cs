using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Model.Entity;

namespace PrivateTutoringApplication.Shared.DTO
{
    public class TutorLessonDTO : BaseModelDTO
    {
        [DataMember]
        public int KullaniciId { get; set; }

        [DataMember]
        public int LessonId { get; set; }

        [DataMember]
        public virtual Kullanici Kullanici { get; set; }

        [DataMember]
        public virtual Lesson Lesson { get; set; }
    }
}
