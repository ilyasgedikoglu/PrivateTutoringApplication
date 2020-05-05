using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface ITutorScheduleService
    {
        TutorScheduleDTO GetById(int id);
        TutorScheduleDTO GetByGuid(Guid guid);
        int Create(TutorScheduleDTO tutorSchedule);
        int Update(TutorScheduleDTO tutorSchedule);
        void Delete(int id);
        IEnumerable<TutorScheduleDTO> GetTutorSchedules();
    }
}
