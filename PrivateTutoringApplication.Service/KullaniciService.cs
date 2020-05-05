using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Service
{
    public class KullaniciService : IKullaniciService
    {
        private readonly IKullaniciRepository _kullaniciRepository;

        public KullaniciService(IKullaniciRepository kullaniciRepository)
        {
            this._kullaniciRepository = kullaniciRepository;
        }

        public bool KullaniciYetkiKontrol(int kullaniciId, int[] yetkiler)
        {
            return _kullaniciRepository.KullaniciYetkiKontrol(kullaniciId, yetkiler);
        }

        public KullaniciDTO GetByKullanici(string email, string sifre)
        {
            var kullanici = _kullaniciRepository.GetByEmail(email);
            var sifreliMetin = "";
            if (kullanici != null)
            {
                sifreliMetin = Sifrele(sifre, kullanici.TuzlamaDegeri);
            }

            return _kullaniciRepository.GetByKullanici(email, sifreliMetin);
        }

        public KullaniciDTO GetByEmail(string email)
        {
            return _kullaniciRepository.GetByEmail(email);
        }

        public void Delete(int id)
        {
            _kullaniciRepository.Delete(id);
        }

        public string Sifrele(string metin, string tuzlamaDegeri)
        {
            var sifrelenmisMetin = KeyDerivation.Pbkdf2(
                password: metin,
                salt: Encoding.UTF8.GetBytes(tuzlamaDegeri),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(sifrelenmisMetin);
        }

        public string GetTuzlamaDegeri()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public KullaniciDTO GetByGuid(Guid guid)
        {
            return _kullaniciRepository.GetByGuid(guid);
        }

        public int Create(KullaniciDTO dto)
        {
            return _kullaniciRepository.Create(dto);
        }

        public int Update(KullaniciDTO kullanici)
        {
            return _kullaniciRepository.Update(kullanici);
        }

        public KullaniciDTO GetById(int id)
        {
            return _kullaniciRepository.GetById(id);
        }

        public IEnumerable<KullaniciDTO> GetByTeachers()
        {
            return _kullaniciRepository.GetByTeachers();
        }

        public IEnumerable<KullaniciDTO> GetByStudents()
        {
            return _kullaniciRepository.GetByStudents();
        }

        public KullaniciDTO GetByTeacher(Guid guid)
        {
            return _kullaniciRepository.GetByTeacher(guid);
        }

        public IEnumerable<KullaniciDTO> GetByTeacherOnaylama()
        {
            return _kullaniciRepository.GetByTeacherOnaylama();
        }
    }
}
