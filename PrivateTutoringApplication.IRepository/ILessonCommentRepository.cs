using System;
using System.Collections.Generic;
using System.Text;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.IRepository
{
    public interface ILessonCommentRepository : ICrud<LessonCommentDTO>
    {
        List<LessonCommentDTO> GetByLessonComment(Guid lessonGuid);
    }
}
