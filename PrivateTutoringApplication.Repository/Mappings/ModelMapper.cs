using AutoMapper;
using PrivateTutoringApplication.Model.Entity;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Repository.Mappings
{
    internal static class ModelMapper
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Kullanici, KullaniciDTO>();
                        cfg.CreateMap<Kullanici, KullaniciDTO>().ReverseMap();

                        cfg.CreateMap<Giris, GirisDTO>();
                        cfg.CreateMap<Giris, GirisDTO>().ReverseMap();

                        cfg.CreateMap<LessonComment, LessonCommentDTO>();
                        cfg.CreateMap<LessonComment, LessonCommentDTO>().ReverseMap();

                        cfg.CreateMap<Lesson, LessonDTO>();
                        cfg.CreateMap<Lesson, LessonDTO>().ReverseMap();

                        cfg.CreateMap<Schedule, ScheduleDTO>();
                        cfg.CreateMap<Schedule, ScheduleDTO>().ReverseMap();

                        cfg.CreateMap<TutorLesson, TutorLessonDTO>();
                        cfg.CreateMap<TutorLesson, TutorLessonDTO>().ReverseMap();

                        cfg.CreateMap<TutorSchedule, TutorScheduleDTO>();
                        cfg.CreateMap<TutorSchedule, TutorScheduleDTO>().ReverseMap();
                    });

                    _mapper = config.CreateMapper();
                }
                return _mapper;
            }
        }


    }
}
