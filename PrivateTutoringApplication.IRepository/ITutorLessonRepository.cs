using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface ITutorLessonRepository : ICrud<TutorLessonDTO>
    {
        IEnumerable<TutorLessonDTO> GetByLessons(Guid guid);
        IEnumerable<TutorLessonDTO> Search(string text); 
        TutorLessonDTO GetByTutorLesson(Guid guid);
    }
}
