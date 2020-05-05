using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.Model.Entity;
using PrivateTutoringApplication.Model.Infrastructure;
using PrivateTutoringApplication.Repository.Mappings;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Repository
{
    public class GirisRepository : IGirisRepository
    {
        private readonly IRepository<Giris> _girisRepository;
        private readonly DatabaseContext _context;
        public GirisRepository(IRepository<Giris> girisRepository, DatabaseContext context)
        {
            _girisRepository = girisRepository;
            _context = context;
        }

        public int Create(GirisDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Giris>(dto);
            entity.EklenmeZamani = DateTime.Now;
            entity.Guid = Guid.NewGuid();
            entity.Kullanici = null;
            entity.EntityState = EntityState.Added;
            return _girisRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Girisler.FirstOrDefault(x => x.Id == id && !x.Silindi);
            entity.Silindi = true;
            entity.EntityState = EntityState.Modified;
            _girisRepository.Save(entity);
        }

        public GirisDTO GetByGuid(Guid guid)
        {
            var entity = _context.Girisler.FirstOrDefault(x => x.Guid == guid && !x.Silindi);
            var dto = ModelMapper.Mapper.Map<GirisDTO>(entity);
            return dto;
        }

        public GirisDTO GetById(int id)
        {
            var entity = _context.Girisler.FirstOrDefault(x => x.Id == id && !x.Silindi);
            var dto = ModelMapper.Mapper.Map<GirisDTO>(entity);
            return dto;
        }

        public bool TokenKontrol(string token)
        {
            return _girisRepository.GetSingle(x => x.Token == token && !x.Silindi).Durum;
        }

        public GirisDTO KullanicininSonTokenBilgisi(int kullaniciId)
        {
            var girisEntity = _context.Girisler.Where(x => !x.Silindi && x.Aktif && x.KullaniciId == kullaniciId)
                .OrderByDescending(o => o.EklenmeZamani).First();
            var dto = ModelMapper.Mapper.Map<GirisDTO>(girisEntity);

            return dto;
        }

        public int Update(GirisDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Giris>(dto);
            entity.EklenmeZamani = DateTime.Now;
            entity.Kullanici = null;
            entity.EntityState = EntityState.Modified;
            return _girisRepository.Save(entity);
        }
    }
}
