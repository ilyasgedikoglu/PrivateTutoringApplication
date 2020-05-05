using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.IService;

namespace PrivateTutoringApplication.Service
{
    public class LessonCommentService : ILessonCommentService
    {
        private readonly ILessonCommentRepository _lessonCommentRepository;

        public LessonCommentService(ILessonCommentRepository lessonCommentRepository)
        {
            _lessonCommentRepository = lessonCommentRepository;
        }

        public LessonCommentDTO GetById(int id)
        {
            return _lessonCommentRepository.GetById(id);
        }

        public LessonCommentDTO GetByGuid(Guid guid)
        {
            return _lessonCommentRepository.GetByGuid(guid);
        }

        public int Create(LessonCommentDTO lesson)
        {
            return _lessonCommentRepository.Create(lesson);
        }

        public int Update(LessonCommentDTO lesson)
        {
            return _lessonCommentRepository.Update(lesson);
        }

        public void Delete(int id)
        {
            _lessonCommentRepository.Delete(id);
        }

        public List<LessonCommentDTO> GetByLessonComment(Guid lessonGuid)
        {
            return _lessonCommentRepository.GetByLessonComment(lessonGuid);
        }
    }
}
