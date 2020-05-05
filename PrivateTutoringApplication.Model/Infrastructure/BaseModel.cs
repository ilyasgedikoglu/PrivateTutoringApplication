using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PrivateTutoringApplication.Model.Entity
{
    public class BaseModel
    {
        [NotMapped]
        public EntityState EntityState { get; set; }
        public DateTime EklenmeZamani { get; set; }
        public Guid Guid { get; set; }
        public bool Aktif { get; set; }
        public bool Silindi { get; set; }
    }
}
