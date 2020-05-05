using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Service
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;

        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }

        public ScheduleDTO GetById(int id)
        {
            return _scheduleRepository.GetById(id);
        }

        public ScheduleDTO GetByGuid(Guid guid)
        {
            return _scheduleRepository.GetByGuid(guid);
        }

        public int Create(ScheduleDTO lesson)
        {
            return _scheduleRepository.Create(lesson);
        }

        public int Update(ScheduleDTO lesson)
        {
            return _scheduleRepository.Update(lesson);
        }

        public void Delete(int id)
        {
            _scheduleRepository.Delete(id);
        }

        public IEnumerable<ScheduleDTO> GetByAll()
        {
            return _scheduleRepository.GetByAll();
        }
    }
}
