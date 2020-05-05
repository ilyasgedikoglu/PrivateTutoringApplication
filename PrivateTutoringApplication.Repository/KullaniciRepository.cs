using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.Model.Entity;
using PrivateTutoringApplication.Model.Infrastructure;
using PrivateTutoringApplication.Repository.Mappings;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.IRepository
{
    public class KullaniciRepository : IKullaniciRepository
    {
        private readonly IRepository<Kullanici> _kullaniciRepository;
        private readonly DatabaseContext _context;

        public KullaniciRepository(IRepository<Kullanici> kullaniciRepository, DatabaseContext context)
        {
            _kullaniciRepository = kullaniciRepository;
            _context = context;
        }

        public bool KullaniciYetkiKontrol(int kullaniciId, int[] yetkiler)
        {
            var sonuc = _context.Kullanicilar.FirstOrDefault(u =>
                !u.Silindi && u.YetkiId.HasValue && yetkiler.Contains(u.YetkiId.Value) && u.Id == kullaniciId);
            if (sonuc == null)
            {
                return false;
            }
            return true;
        }

        public KullaniciDTO GetByKullanici(string email, string sifre)
        {

            var kullanici = _context.Kullanicilar
                .Where(x => !x.Silindi && x.Email != null && x.Email == email && x.Sifre == sifre).Include(y => y.Yetki)
                .FirstOrDefault();
            return ModelMapper.Mapper.Map<KullaniciDTO>(kullanici);
        }

        public KullaniciDTO GetByEmail(string email)
        {
            var kullanici = _context.Kullanicilar.Where(x => !x.Silindi && x.Email != null && x.Email == email)
                .Include(x => x.Yetki).FirstOrDefault();
            return ModelMapper.Mapper.Map<KullaniciDTO>(kullanici);
        }

        public KullaniciDTO GetById(int id)
        {
            var user = _context.Kullanicilar.Where(u => !u.Silindi && u.Id == id).Include(x => x.Yetki).FirstOrDefault();
            return ModelMapper.Mapper.Map<KullaniciDTO>(user);
        }

        public KullaniciDTO GetByGuid(Guid guid)
        {
            var user = _context.Kullanicilar.FirstOrDefault(u => !u.Silindi && u.Guid == guid);
            return ModelMapper.Mapper.Map<KullaniciDTO>(user);
        }

        public IEnumerable<KullaniciDTO> GetByTeachers()
        {
            var entity = _context.Kullanicilar.Where(x => !x.Silindi && x.YetkiId == (int) Yetkiler.TEACHER && x.Aktif)
                .Include(x => x.Yetki).ToList();
            return ModelMapper.Mapper.Map<IEnumerable<KullaniciDTO>>(entity);
        }

        public KullaniciDTO GetByTeacher(Guid guid)
        {
            var entity = _context.Kullanicilar.FirstOrDefault(x => !x.Silindi && x.Guid == guid && x.YetkiId == (int)Yetkiler.TEACHER && x.Aktif);
            return ModelMapper.Mapper.Map<KullaniciDTO>(entity);
        }

        public IEnumerable<KullaniciDTO> GetByTeacherOnaylama()
        {
            var entity = _context.Kullanicilar.Where(x => !x.Silindi && x.YetkiId == (int)Yetkiler.TEACHER && !x.Aktif).OrderByDescending(x=>x.EklenmeZamani).ToList();
            return ModelMapper.Mapper.Map<IEnumerable<KullaniciDTO>>(entity);
        }

        public IEnumerable<KullaniciDTO> GetByStudents()
        {
            var entity = _context.Kullanicilar.Where(x => !x.Silindi && x.YetkiId == (int)Yetkiler.STUDENT && x.Aktif)
                .Include(x => x.Yetki).ToList();
            return ModelMapper.Mapper.Map<IEnumerable<KullaniciDTO>>(entity);
        }

        public int Create(KullaniciDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Kullanici>(dto);
            entity.Yetki = null;
            entity.EntityState = EntityState.Added;
            return _kullaniciRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Kullanicilar.FirstOrDefault(x => x.Id == id);
            entity.Silindi = true;
            entity.Aktif = false;
            entity.EntityState = EntityState.Modified;
            _kullaniciRepository.Save(entity);
        }

        public int Update(KullaniciDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Kullanici>(dto);
            entity.EntityState = EntityState.Modified;
            return _kullaniciRepository.Save(entity);
        }
    }
}
