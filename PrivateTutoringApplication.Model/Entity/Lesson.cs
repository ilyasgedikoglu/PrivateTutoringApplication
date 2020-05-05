using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PrivateTutoringApplication.Model.Entity
{
    [Table("Lesson")]
    public partial class Lesson : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(63)]
        public string Name { get; set; }

        [MaxLength(63)]
        public string Category { get; set; }

        [MaxLength(127)]
        public string Level { get; set; }

        public string Definition { get; set; }

        public double Price { get; set; }

        public int Discount { get; set; }

        public string Resim { get; set; }
    }
}
