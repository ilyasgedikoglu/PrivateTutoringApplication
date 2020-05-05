using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrivateTutoringApplication.Model.Entity
{
    [Table("Schedule")]
    public partial class Schedule : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int TutorScheduleId { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual TutorSchedule TutorSchedule { get; set; }
    }
}
