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
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Repository
{
    public class TutorLessonRepository : ITutorLessonRepository
    {
        private readonly IRepository<TutorLesson> _tutorLessonRepository;
        private readonly DatabaseContext _context;

        public TutorLessonRepository(IRepository<TutorLesson> tutorLessonRepository, DatabaseContext context)
        {
            _context = context;
            _tutorLessonRepository = tutorLessonRepository;
        }

        public TutorLessonDTO GetById(int id)
        {
            var tutorLesson = _context.TutorLessons.Where(u => !u.Silindi && u.Id == id).Include(x=>x.Kullanici).Include(x=>x.Lesson).FirstOrDefault();
            return ModelMapper.Mapper.Map<TutorLessonDTO>(tutorLesson);
        }

        public TutorLessonDTO GetByGuid(Guid guid)
        {
            var tutorLesson = _context.TutorLessons.Where(u => !u.Silindi && u.Guid == guid).Include(x=>x.Lesson).Include(x=>x.Kullanici).FirstOrDefault();
            return ModelMapper.Mapper.Map<TutorLessonDTO>(tutorLesson);
        }

        public int Create(TutorLessonDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<TutorLesson>(dto);
            entity.EntityState = EntityState.Added;
            return _tutorLessonRepository.Save(entity);
        }

        public void Delete(int id)
        {
            var entity = _context.TutorLessons.FirstOrDefault(x => x.Id == id);
            entity.Silindi = true;
            entity.Aktif = false;
            entity.EntityState = EntityState.Modified;
            _tutorLessonRepository.Save(entity);
        }

        public int Update(TutorLessonDTO dto)
        {
            var entity = ModelMapper.Mapper.Map<TutorLesson>(dto);
            entity.EntityState = EntityState.Modified;
            return _tutorLessonRepository.Save(entity);
        }

        public IEnumerable<TutorLessonDTO> GetByLessons(Guid guid)
        {
            var tutorLessons = _context.TutorLessons.Where(u => !u.Silindi && u.Kullanici.Guid == guid)
                .Include(x => x.Lesson).Include(x => x.Kullanici).ToList();
            return ModelMapper.Mapper.Map<IEnumerable<TutorLessonDTO>>(tutorLessons);
        }

        public IEnumerable<TutorLessonDTO> Search(string text)
        {
            var searhResult = _context.TutorLessons.Where(x =>
                !x.Silindi && !x.Kullanici.Silindi && !x.Lesson.Silindi &&
                (x.Lesson.Name.ToLower().Contains(text) || x.Lesson.Category.ToLower().Contains(text) ||
                 ((x.Kullanici.Ad.ToLower().Contains(text) || x.Kullanici.Soyad.ToLower().Contains(text)) &&
                  x.Kullanici.YetkiId == (int) Yetkiler.TEACHER))).Include(x=>x.Kullanici).Include(x=>x.Lesson);

            return ModelMapper.Mapper.Map<IEnumerable<TutorLessonDTO>>(searhResult);
        }

        public TutorLessonDTO GetByTutorLesson(Guid guid)
        {
            var lesson = _context.Lessons.FirstOrDefault(u => !u.Silindi && u.Guid == guid);
            var tutorLesson = _context.TutorLessons.Where(u => !u.Silindi && lesson.Id == u.LessonId).Include(x=>x.Kullanici).Include(x=>x.Lesson).FirstOrDefault();
            return ModelMapper.Mapper.Map<TutorLessonDTO>(tutorLesson);
        }
    }
}
