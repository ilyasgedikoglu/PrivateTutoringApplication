using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface IKullaniciRepository : ICrud<KullaniciDTO>
    { 
        bool KullaniciYetkiKontrol(int kullaniciId, int[] yetkiler);

        KullaniciDTO GetByEmail(string email);

        KullaniciDTO GetByKullanici(string email, string sifre);
        IEnumerable<KullaniciDTO> GetByTeachers();
        KullaniciDTO GetByTeacher(Guid guid);
        IEnumerable<KullaniciDTO> GetByStudents();
        IEnumerable<KullaniciDTO> GetByTeacherOnaylama();
    }
}
