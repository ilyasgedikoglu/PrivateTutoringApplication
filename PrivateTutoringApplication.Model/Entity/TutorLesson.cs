using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrivateTutoringApplication.Model.Entity
{
    [Table("KullaniciLesson")]
    public partial class TutorLesson : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int LessonId { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
