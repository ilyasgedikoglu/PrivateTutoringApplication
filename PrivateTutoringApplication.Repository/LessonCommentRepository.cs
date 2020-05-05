using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.Model.Entity;
using PrivateTutoringApplication.Model.Infrastructure;
using PrivateTutoringApplication.Repository.Mappings;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Repository
{
    public class LessonCommentRepository : ILessonCommentRepository
    {
        private readonly IRepository<LessonComment> _lessonCommentRepository;
        private readonly DatabaseContext _context;

        public LessonCommentRepository(IRepository<LessonComment> lessonCommentRepository, DatabaseContext context)
        {
            _context = context;
            _lessonCommentRepository = lessonCommentRepository;
        }

        public LessonCommentDTO GetById(int id)
        {
            var lessonComment = _context.LessonComments.FirstOrDefault(u => !u.Silindi && u.Id == id);
            return ModelMapper.Mapper.Map<LessonCommentDTO>(lessonComment);
        }

        public LessonCommentDTO GetByGuid(Guid guid)
        {
            var lessonComment = _context.LessonComments.FirstOrDefault(u => !u.Silindi && u.Guid == guid);
            return ModelMapper.Mapper.Map<LessonCommentDTO>(lessonComment);
        }

        public int Create(LessonCommentDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<LessonComment>(dto);
            entity.EntityState = EntityState.Added;
            return _lessonCommentRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.LessonComments.FirstOrDefault(x => x.Id == id);
            entity.Silindi = true;
            entity.Aktif = false;
            entity.EntityState = EntityState.Modified;
            _lessonCommentRepository.Save(entity);
        }

        public int Update(LessonCommentDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<LessonComment>(dto);
            entity.EntityState = EntityState.Modified;
            return _lessonCommentRepository.Save(entity);
        }

        public List<LessonCommentDTO> GetByLessonComment(Guid lessonGuid)
        {
            var entity = _context.LessonComments.Where(x => !x.Silindi && x.Lesson.Guid == lessonGuid)
                .Include(x => x.Lesson).Include(x => x.Kullanici).ToList();
            return ModelMapper.Mapper.Map<List<LessonCommentDTO>>(entity);
        }
    }
}
