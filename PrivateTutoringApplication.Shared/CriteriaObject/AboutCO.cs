using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PrivateTutoringApplication.Shared.CriteriaObject
{
    [DataContract]
    [Serializable]
    public class AboutCO
    {
        [DataMember]
        public int TeacherCount { get; set; }

        [DataMember]
        public int StudentCount { get; set; }

        [DataMember]
        public int LessonCount { get; set; }

        [DataMember]
        public int AppointmentCount { get; set; }
    }
}
