using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Model.Entity;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface ILessonRepository : ICrud<LessonDTO>
    {
        IEnumerable<LessonDTO> GetLessons();
    }
}
