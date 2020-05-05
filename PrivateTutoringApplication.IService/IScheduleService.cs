using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface IScheduleService
    {
        ScheduleDTO GetById(int id);
        ScheduleDTO GetByGuid(Guid guid);
        int Create(ScheduleDTO schedule);
        int Update(ScheduleDTO schedule);
        void Delete(int id);

        IEnumerable<ScheduleDTO> GetByAll();
    }
}
