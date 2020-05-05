using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using PrivateTutoringApplication.Model.Entity;

namespace PrivateTutoringApplication.Shared.DTO
{
    [DataContract]
    [Serializable]
    public class ScheduleDTO : BaseModelDTO
    {
        [DataMember]
        public int KullaniciId { get; set; }

        [DataMember]
        public int TutorScheduleId { get; set; }

        [DataMember]
        public virtual Kullanici Kullanici { get; set; }

        [DataMember]
        public virtual TutorSchedule TutorSchedule { get; set; }
    }
}
