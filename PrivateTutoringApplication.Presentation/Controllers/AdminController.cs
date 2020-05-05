using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Model.Infrastructure;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    public class AdminController : Controller
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly ILessonService _lessonService;
        private readonly IScheduleService _scheduleService;

        public AdminController(IKullaniciService kullaniciService, ILessonService lessonService, IScheduleService scheduleService)
        {
            _kullaniciService = kullaniciService;
            _lessonService = lessonService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.ADMIN } })]
        public IActionResult Index()
        {
            var teacherOnay = _kullaniciService.GetByTeacherOnaylama();
            return View(teacherOnay);
        }

        [HttpGet]
        public ActionResult _headerAdminPartial()
        {
            return PartialView();
        }

        [HttpGet]
        public ActionResult _sideBarAdminPartial()
        {
            return PartialView();
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.ADMIN } })]
        public ActionResult GetLessons()
        {
            var lessons = _lessonService.GetLessons();
            return View(lessons);
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.ADMIN } })]
        public ActionResult GetTeachers()
        {
            var teachers = _kullaniciService.GetByTeachers();
            return View(teachers);
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.ADMIN } })]
        public ActionResult GetStudents()
        {
            var students = _kullaniciService.GetByStudents();
            return View(students);
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.ADMIN } })]
        public ActionResult GetRegisterLesson()
        {
            var registerLessons = _scheduleService.GetByAll();
            return View(registerLessons);
        }
    }
}