using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    [AllowAnonymous]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IKullaniciService _kullaniciService;
        private readonly ITutorScheduleService _tutorScheduleService;
        private readonly ITutorLessonService _tutorLessonService;

        public ScheduleController(IScheduleService scheduleService, IKullaniciService kullaniciService, ITutorScheduleService tutorScheduleService, ITutorLessonService tutorLessonService)
        {
            _scheduleService = scheduleService;
            _kullaniciService = kullaniciService;
            _tutorScheduleService = tutorScheduleService;
            _tutorLessonService = tutorLessonService;
        }

        //Kullanıcının randevuları listelenecek
        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.STUDENT, (int)Yetkiler.TEACHER } })]
        public ActionResult GetAppointments()
        {
            var user = _kullaniciService.GetById(Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault()));

            if (user.YetkiId == 2)
            {
                var silineceklerr = _scheduleService.GetByAll().Where(x =>
                    !x.Silindi && x.Aktif && x.KullaniciId == user.Id && x.TutorSchedule.LessonStartDate < DateTime.Now).ToList();
                if (silineceklerr.Count > 0)
                {
                    foreach (var item in silineceklerr)
                    {
                        _scheduleService.Delete(item.Id);
                    }
                }

                var appointmentss = _scheduleService.GetByAll()
                    .Where(x => !x.Silindi && x.Aktif && x.KullaniciId == user.Id)
                    .OrderByDescending(x => x.TutorSchedule.LessonStartDate).ToList();

                return View("IndexStudent", appointmentss);
            }

            var silinecekler = _scheduleService.GetByAll().Where(x =>
                !x.Silindi && x.Aktif && x.KullaniciId == user.Id && x.TutorSchedule.LessonStartDate < DateTime.Now).ToList();
            if (silinecekler.Count > 0)
            {
                foreach (var item in silinecekler)
                {
                    _scheduleService.Delete(item.Id);
                }
            }

            var appointments = _scheduleService.GetByAll()
                .Where(x => !x.Silindi && x.Aktif && x.TutorSchedule.Kullanici.Guid == user.Guid)
                .OrderByDescending(x => x.TutorSchedule.LessonStartDate);

            return View("IndexTeacher", appointments);
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.STUDENT } })]
        public IActionResult Create(Guid guid) //tutorlesson guid
        {
            var tutorLesson = _tutorLessonService.GetByGuid(guid);
            var tutorSchedule = _tutorScheduleService.GetTutorSchedules().Where(x => x.LessonId == tutorLesson.LessonId && x.KullaniciId == tutorLesson.KullaniciId).ToList();

            ViewBag.TutorSchedules = tutorSchedule;
            return View();
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.STUDENT } })]
        public ActionResult Create(ScheduleDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.Guid = Guid.NewGuid();
            model.EklenmeZamani = DateTime.Now;
            model.Aktif = true;
            model.Silindi = false;
            model.KullaniciId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());

            _scheduleService.Create(model);

            return RedirectToAction("GetAppointments");
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.STUDENT } })]
        public ActionResult Edit(Guid guid)
        {
            var model = _scheduleService.GetByGuid(guid);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.STUDENT } })]
        public ActionResult Edit(ScheduleDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            _scheduleService.Update(model);

            return RedirectToAction("GetAppointments");
        }

        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.STUDENT, (int)Yetkiler.TEACHER } })]
        public ActionResult Delete(Guid guid)
        {
            var schedule = _scheduleService.GetByGuid(guid);
            _scheduleService.Delete(schedule.Id);

            return RedirectToAction("GetAppointments");
        }
    }
}