using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface ITutorLessonService
    {
        TutorLessonDTO GetById(int id);
        TutorLessonDTO GetByGuid(Guid guid);
        int Create(TutorLessonDTO tutorLesson);
        int Update(TutorLessonDTO tutorLesson);
        void Delete(int id);
        IEnumerable<TutorLessonDTO> GetByLessons(Guid guid);
        IEnumerable<TutorLessonDTO> Search(string text);
        TutorLessonDTO GetByTutorLesson(Guid guid);
    }
}
