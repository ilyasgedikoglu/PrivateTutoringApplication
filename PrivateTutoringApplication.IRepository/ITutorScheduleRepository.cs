using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface ITutorScheduleRepository : ICrud<TutorScheduleDTO>
    {
        IEnumerable<TutorScheduleDTO> GetTutorSchedules();
    }
}
