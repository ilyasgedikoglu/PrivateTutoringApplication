using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface ILessonService
    {
        LessonDTO GetById(int id);
        LessonDTO GetByGuid(Guid guid);
        int Create(LessonDTO lesson);
        int Update(LessonDTO lesson);
        void Delete(int id);
        IEnumerable<LessonDTO> GetLessons();
    }
}
