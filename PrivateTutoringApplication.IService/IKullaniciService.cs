using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface IKullaniciService
    {
        KullaniciDTO GetById(int id);
        KullaniciDTO GetByGuid(Guid guid);
        //KullaniciDTO GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre);
        //KullaniciDTO GetByKullaniciAdi(string kullaniciAdi);
        int Create(KullaniciDTO kullanici);
        int Update(KullaniciDTO kullanici);
        void Delete(int id);
        string Sifrele(string metin, string tuzlamaDegeri);
        string GetTuzlamaDegeri();
        KullaniciDTO GetByKullanici(string email, string sifre);

        //SayfalamaDTO<KullaniciDTO> GetKullaniciList(KullaniciCO co);
        //List<KullaniciDTO> GetKullanicilar();
        //int KullaniciSayisiByYetkiId(int yetkiId);
        bool KullaniciYetkiKontrol(int kullaniciId, int[] yetkiler);
        IEnumerable<KullaniciDTO> GetByTeachers();
        KullaniciDTO GetByTeacher(Guid guid);
        IEnumerable<KullaniciDTO> GetByStudents();
        IEnumerable<KullaniciDTO> GetByTeacherOnaylama();

        KullaniciDTO GetByEmail(string email);
    }
}
