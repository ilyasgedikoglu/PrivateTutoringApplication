using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    [AllowAnonymous]
    public class TutorScheduleController : Controller
    {
        private readonly ITutorScheduleService _tutorScheduleService;
        private readonly ITutorLessonService _tutorLessonService;
        private readonly ILessonService _lessonService;

        public TutorScheduleController(ITutorScheduleService tutorScheduleService, ILessonService lessonService, ITutorLessonService tutorLessonService)
        {
            _tutorScheduleService = tutorScheduleService;
            _lessonService = lessonService;
            _tutorLessonService = tutorLessonService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Create(TutorScheduleDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.Guid = Guid.NewGuid();
            model.EklenmeZamani = DateTime.Now;
            model.Aktif = true;
            model.Silindi = false;
            model.KullaniciId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());

            _tutorScheduleService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Edit(Guid guid)
        {
            var model = _tutorScheduleService.GetByGuid(guid);
            TempData["TutorScheduleId"] = model.Id;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Edit(TutorScheduleDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            _tutorScheduleService.Update(model);
            return RedirectToAction("Index");
        }

        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Delete(Guid guid)
        {
            var item = _tutorScheduleService.GetByGuid(guid);
            _tutorScheduleService.Delete(item.Id);
            return RedirectToAction("Index", "TutorSchedule");
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult LessonHours(Guid guid)
        {
            var tutorLesson = _tutorLessonService.GetByGuid(guid);
            TutorScheduleDTO model = new TutorScheduleDTO();
            model.Guid = Guid.NewGuid();
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.Guid = Guid.NewGuid();
            model.EklenmeZamani = DateTime.Now;
            model.Aktif = true;
            model.Silindi = false;
            model.KullaniciId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.LessonId = _lessonService.GetById(tutorLesson.LessonId).Id;
            
            var id = _tutorScheduleService.Create(model);
            var tutorSchedule = _tutorScheduleService.GetById(id);
            return RedirectToAction("AddLessonHours", new RouteValueDictionary(new { controller = "TutorSchedule", action = "AddLessonHours", guid = tutorSchedule.Guid }));
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult AddLessonHours(Guid guid)
        {
            var tutorSchedule = _tutorScheduleService.GetByGuid(guid);
            return View(tutorSchedule);
        }


        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult AddLessonHours(TutorScheduleDTO model)
        {
            var tutorSchedule = _tutorScheduleService.GetById(model.Id);
            model.Guid = tutorSchedule.Guid;
            model.EkleyenId = tutorSchedule.EkleyenId;
            model.EklenmeZamani = tutorSchedule.EklenmeZamani;
            model.Aktif = tutorSchedule.Aktif;
            model.Silindi = tutorSchedule.Silindi;
            model.KullaniciId = tutorSchedule.KullaniciId;
            model.LessonId = tutorSchedule.LessonId;

            _tutorScheduleService.Update(model);
            var lesson = _lessonService.GetById(tutorSchedule.LessonId);
            return RedirectToAction("GetLesson2", new RouteValueDictionary(new { controller = "Lesson", action = "GetLesson2", guid = lesson.Guid }));
        }
    }
}