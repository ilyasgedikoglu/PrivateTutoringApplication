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
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IRepository<Schedule> _scheduleRepository;
        private readonly DatabaseContext _context;

        public ScheduleRepository(IRepository<Schedule> scheduleRepository, DatabaseContext context)
        {
            _context = context;
            _scheduleRepository = scheduleRepository;
        }

        public ScheduleDTO GetById(int id)
        {
            var schedule = _scheduleRepository.GetSingle(u => !u.Silindi && u.Id == id);
            return ModelMapper.Mapper.Map<ScheduleDTO>(schedule);
        }

        public ScheduleDTO GetByGuid(Guid guid)
        {
            var schedule = _scheduleRepository.GetSingle(u => !u.Silindi && u.Guid == guid);
            return ModelMapper.Mapper.Map<ScheduleDTO>(schedule);
        }

        public int Create(ScheduleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Schedule>(dto);
            entity.EntityState = EntityState.Added;
            return _scheduleRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _scheduleRepository.GetSingle(x => x.Id == id);
            entity.Silindi = true;
            entity.Aktif = false;
            entity.EntityState = EntityState.Modified;
            _scheduleRepository.Save(entity);
        }

        public int Update(ScheduleDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Schedule>(dto);
            entity.EntityState = EntityState.Modified;
            return _scheduleRepository.Save(entity);
        }

        public IEnumerable<ScheduleDTO> GetByAll()
        {
            var entities = _context.Schedules
                .Where(x => !x.Silindi && !x.Kullanici.Silindi && !x.TutorSchedule.Silindi)
                    .Include(x => x.Kullanici).Include(x => x.TutorSchedule).Include(x => x.TutorSchedule.Kullanici)
                .Include(x => x.TutorSchedule.Lesson);

            return ModelMapper.Mapper.Map<IEnumerable<ScheduleDTO>>(entities);
        }
    }
}
