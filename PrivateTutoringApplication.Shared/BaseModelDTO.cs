using System;
using System.Runtime.Serialization;

namespace PrivateTutoringApplication.Shared
{
    public abstract class BaseModelDTO
    { 
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public bool Aktif { get; set; }

        public bool Silindi { get; set; }

        public int? EkleyenId { get; set; }

        public DateTime EklenmeZamani { get; set; }
    }
}
