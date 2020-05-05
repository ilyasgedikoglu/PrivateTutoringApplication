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
    public class YetkiRepository : IYetkiRepository
    {
        private readonly IRepository<Yetki> _yetkiRepository;
        private readonly IRepository<Kullanici> _kullaniciRepository;
        private readonly DatabaseContext _context;

        public YetkiRepository(IRepository<Yetki> yetkiRepository, DatabaseContext context,
            IRepository<Kullanici> kullaniciRepository)
        {
            this._yetkiRepository = yetkiRepository;
            _context = context;
            _kullaniciRepository = kullaniciRepository;
        }

        public YetkiDTO GetByGuid(Guid guid)
        {
            //var entity = _yetkiRepository.GetSingle(x => x.Guid == guid && !x.Silindi);
            var entity = _context.Yetkiler.Where(x => x.Guid == guid && !x.Silindi).AsNoTracking().FirstOrDefault();

            var dto = ModelMapper.Mapper.Map<YetkiDTO>(entity);
            return dto;
        }

        public int Create(YetkiDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Yetki>(dto);
            entity.EklenmeZamani = DateTime.Now;
            entity.Guid = Guid.NewGuid();
            entity.EntityState = EntityState.Added;
            return _yetkiRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _yetkiRepository.GetSingle(x => x.Id == id && !x.Silindi);
            entity.Silindi = true;
            entity.EntityState = EntityState.Modified;
            _yetkiRepository.Save(entity);
        }

        public YetkiDTO GetById(int id)
        {
            var entity = _yetkiRepository.GetSingle(x => x.Id == id && !x.Silindi);
            var dto = ModelMapper.Mapper.Map<YetkiDTO>(entity);
            return dto;
        }

        public int Update(YetkiDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Yetki>(dto);
            entity.EntityState = EntityState.Modified;
            return _yetkiRepository.Save(entity);
        }
    }
}
