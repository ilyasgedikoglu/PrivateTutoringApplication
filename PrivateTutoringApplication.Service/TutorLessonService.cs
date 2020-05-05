using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Service
{
    public class TutorLessonService : ITutorLessonService
    {
        private readonly ITutorLessonRepository _tutorLessonRepository;

        public TutorLessonService(ITutorLessonRepository tutorLessonRepository)
        {
            _tutorLessonRepository = tutorLessonRepository;
        }

        public TutorLessonDTO GetById(int id)
        {
            return _tutorLessonRepository.GetById(id);
        }

        public TutorLessonDTO GetByGuid(Guid guid)
        {
            return _tutorLessonRepository.GetByGuid(guid);
        }

        public int Create(TutorLessonDTO lesson)
        {
            return _tutorLessonRepository.Create(lesson);
        }

        public int Update(TutorLessonDTO lesson)
        {
            return _tutorLessonRepository.Update(lesson);
        }

        public void Delete(int id)
        {
            _tutorLessonRepository.Delete(id);
        }

        public IEnumerable<TutorLessonDTO> GetByLessons(Guid guid)
        {
            return _tutorLessonRepository.GetByLessons(guid);
        }

        public IEnumerable<TutorLessonDTO> Search(string text)
        {
            return _tutorLessonRepository.Search(text);
        }

        public TutorLessonDTO GetByTutorLesson(Guid guid)
        {
            return _tutorLessonRepository.GetByTutorLesson(guid);
        }
    }
}
