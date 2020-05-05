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
    public class LessonRepository : ILessonRepository
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly DatabaseContext _context;

        public LessonRepository(IRepository<Lesson> lessonRepository, DatabaseContext context)
        {
            _context = context;
            _lessonRepository = lessonRepository;
        }

        public LessonDTO GetById(int id)
        {
            var lesson = _context.Lessons.FirstOrDefault(u => !u.Silindi && u.Id == id);
            return ModelMapper.Mapper.Map<LessonDTO>(lesson);
        }

        public LessonDTO GetByGuid(Guid guid)
        {
            var lesson = _context.Lessons.FirstOrDefault(u => !u.Silindi && u.Guid == guid);
            return ModelMapper.Mapper.Map<LessonDTO>(lesson);
        }

        public int Create(LessonDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Lesson>(dto);
            entity.EntityState = EntityState.Added;
            return _lessonRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.Lessons.FirstOrDefault(x => x.Id == id);
            entity.Silindi = true;
            entity.Aktif = false;
            entity.EntityState = EntityState.Modified;
            _lessonRepository.Save(entity);
        }

        public int Update(LessonDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<Lesson>(dto);
            entity.EntityState = EntityState.Modified;
            return _lessonRepository.Save(entity);
        }

        public IEnumerable<LessonDTO> GetLessons()
        {
            var lessons = _context.Lessons.Where(x => !x.Silindi);
            return ModelMapper.Mapper.Map<IEnumerable<LessonDTO>>(lessons);
        }
    }
}
