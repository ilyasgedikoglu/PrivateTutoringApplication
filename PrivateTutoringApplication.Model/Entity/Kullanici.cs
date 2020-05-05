using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateTutoringApplication.Model.Entity
{
    [Table("Kullanici")]
    public partial class Kullanici : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(63)]
        public string Ad { get; set; }

        [MaxLength(63)]
        public string Soyad { get; set; }

        [MaxLength(127)]
        public string KullaniciAdi { get; set; }

        [MaxLength(63)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string TelephoneNumber { get; set; }

        [MaxLength(63)]
        public string Sifre { get; set; }

        [MaxLength(63)]
        public string TuzlamaDegeri { get; set; }

        [MaxLength(31)]
        public string KimlikNo { get; set; }

        [MaxLength(255)]
        public string Resim { get; set; }

        [MaxLength(63)]
        public string DogumYeri { get; set; }

        public DateTime? DogumTarihi { get; set; }

        public int? YetkiId { get; set; }

        public string Aciklama { get; set; }

        public string Biyografi { get; set; }

        public string CVPath { get; set; }

        public string School { get; set; }

        public string UpperSchool { get; set; }

        public string Department { get; set; }

        public string UpperDepartment { get; set; }

        public string Job { get; set; }

        public string GraduationStatus { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        public virtual Yetki Yetki { get; set; }
    }
}
