using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IService
{
    public interface ILessonCommentService
    {
        LessonCommentDTO GetById(int id);
        LessonCommentDTO GetByGuid(Guid guid);
        int Create(LessonCommentDTO lessonComment);
        int Update(LessonCommentDTO lessonComment);
        void Delete(int id);
        List<LessonCommentDTO> GetByLessonComment(Guid lessonGuid);
    }
}
