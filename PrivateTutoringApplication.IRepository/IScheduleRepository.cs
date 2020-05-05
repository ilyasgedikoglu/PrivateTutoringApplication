using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface IScheduleRepository : ICrud<ScheduleDTO>
    {
        IEnumerable<ScheduleDTO> GetByAll();
    }
}
