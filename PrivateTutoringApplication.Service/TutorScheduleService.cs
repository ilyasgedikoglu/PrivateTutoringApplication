using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Service
{
    public class TutorScheduleService : ITutorScheduleService
    {
        private readonly ITutorScheduleRepository _tutorScheduleRepository;

        public TutorScheduleService(ITutorScheduleRepository tutorScheduleRepository)
        {
            _tutorScheduleRepository = tutorScheduleRepository;
        }

        public TutorScheduleDTO GetById(int id)
        {
            return _tutorScheduleRepository.GetById(id);
        }

        public TutorScheduleDTO GetByGuid(Guid guid)
        {
            return _tutorScheduleRepository.GetByGuid(guid);
        }

        public int Create(TutorScheduleDTO lesson)
        {
            return _tutorScheduleRepository.Create(lesson);
        }

        public int Update(TutorScheduleDTO lesson)
        {
            return _tutorScheduleRepository.Update(lesson);
        }

        public void Delete(int id)
        {
            _tutorScheduleRepository.Delete(id);
        }

        public IEnumerable<TutorScheduleDTO> GetTutorSchedules()
        {
            return _tutorScheduleRepository.GetTutorSchedules();
        }
    }
}
