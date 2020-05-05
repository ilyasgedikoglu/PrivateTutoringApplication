using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Service
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            _lessonRepository = lessonRepository;
        }

        public LessonDTO GetById(int id)
        {
            return _lessonRepository.GetById(id);
        }

        public LessonDTO GetByGuid(Guid guid)
        {
            return _lessonRepository.GetByGuid(guid);
        }

        public int Create(LessonDTO lesson)
        {
            return _lessonRepository.Create(lesson);
        }

        public int Update(LessonDTO lesson)
        {
            return _lessonRepository.Update(lesson);
        }

        public void Delete(int id)
        {
            _lessonRepository.Delete(id);
        }

        public IEnumerable<LessonDTO> GetLessons()
        {
            return _lessonRepository.GetLessons();
        }
    }
}
