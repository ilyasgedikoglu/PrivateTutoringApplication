using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Text;

namespace PrivateTutoringApplication.Model.Entity
{
    [Table("Giris")]
    public partial class Giris : BaseModel
    {
        [Key]
        public int Id { get; set; }
        public int KullaniciId { get; set; }

        [MaxLength(511)]
        public string Token { get; set; }
        public bool Durum { get; set; }
        public List<IPAddress> IPAddress { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
