using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Shared.CriteriaObject
{
    [DataContract]
    [Serializable]
    public class TutorLessonCommentCO
    {
        [DataMember]
        public TutorLessonDTO TutorLesson { get; set; }

        [DataMember]
        public IEnumerable<LessonCommentDTO> Comment { get; set; }
    }
}
