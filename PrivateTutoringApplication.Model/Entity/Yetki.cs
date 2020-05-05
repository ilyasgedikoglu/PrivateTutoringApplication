using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateTutoringApplication.Model.Entity
{
    [Table("Yetki")]
    public partial class Yetki : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(63)]
        public string Adi { get; set; }

        [MaxLength(127)]
        public string Aciklama { get; set; }

        public bool Goster { get; set; }
    }
}
