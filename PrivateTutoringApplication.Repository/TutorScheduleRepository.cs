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
    public class TutorScheduleRepository : ITutorScheduleRepository
    {
        private readonly IRepository<TutorSchedule> _tutorScheduleRepository;
        private readonly DatabaseContext _context;

        public TutorScheduleRepository(IRepository<TutorSchedule> tutorScheduleRepository, DatabaseContext context)
        {
            _context = context;
            _tutorScheduleRepository = tutorScheduleRepository;
        }

        public TutorScheduleDTO GetById(int id)
        {
            var tutorSchedule = _context.TutorSchedules.FirstOrDefault(u => !u.Silindi && u.Id == id);
            return ModelMapper.Mapper.Map<TutorScheduleDTO>(tutorSchedule);
        }

        public TutorScheduleDTO GetByGuid(Guid guid)
        {
            var tutorSchedule = _context.TutorSchedules.FirstOrDefault(u => !u.Silindi && u.Guid == guid);
            return ModelMapper.Mapper.Map<TutorScheduleDTO>(tutorSchedule);
        }

        public int Create(TutorScheduleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<TutorSchedule>(dto);
            entity.EntityState = EntityState.Added;
            return _tutorScheduleRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.TutorSchedules.FirstOrDefault(x => x.Id == id);
            entity.Silindi = true;
            entity.Aktif = false;
            entity.EntityState = EntityState.Modified;
            _tutorScheduleRepository.Save(entity);
        }

        public int Update(TutorScheduleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<TutorSchedule>(dto);
            entity.EntityState = EntityState.Modified;
            return _tutorScheduleRepository.Save(entity);
        }

        public IEnumerable<TutorScheduleDTO> GetTutorSchedules()
        {
            var tutorSchedules = _context.TutorSchedules.Where(x => !x.Silindi);
            return ModelMapper.Mapper.Map<IEnumerable<TutorScheduleDTO>>(tutorSchedules);
        }
    }
}
