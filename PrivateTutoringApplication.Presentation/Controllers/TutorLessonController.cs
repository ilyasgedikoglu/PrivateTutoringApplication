using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    [AllowAnonymous]
    public class TutorLessonController : Controller
    {
        private readonly ITutorLessonService _tutorLessonService;

        public TutorLessonController(ITutorLessonService tutorLessonService)
        {
            _tutorLessonService = tutorLessonService;
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
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
        public ActionResult Create(TutorLessonDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.Guid = Guid.NewGuid();
            model.EklenmeZamani = DateTime.Now;
            model.Aktif = true;
            model.Silindi = false;

            _tutorLessonService.Create(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Edit(Guid guid)
        {
            var model = _tutorLessonService.GetByGuid(guid);
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Edit(TutorLessonDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            _tutorLessonService.Update(model);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Delete(int id)
        {
            _tutorLessonService.Delete(id);
            return RedirectToAction("Index", "TutorLesson");
        }
    }
}