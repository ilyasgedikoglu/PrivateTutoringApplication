using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Shared.CriteriaObject;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    [AllowAnonymous]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ITutorLessonService _tutorLessonService;
        private readonly ILessonCommentService _lessonCommentService;
        private readonly IKullaniciService _kullaniciService;

        public LessonController(ILessonService lessonService, ITutorLessonService tutorLessonService, ILessonCommentService lessonCommentService, IKullaniciService kullaniciService)
        {
            _lessonService = lessonService;
            _tutorLessonService = tutorLessonService;
            _lessonCommentService = lessonCommentService;
            _kullaniciService = kullaniciService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lessons = _lessonService.GetLessons();
            return View(lessons);
        }

        //tutor lesson guid geliyor
        [HttpGet]
        public IActionResult GetLesson(Guid guid)
        {
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            if (userId > 0)
            {
                var user = _kullaniciService.GetById(userId);

                ViewBag.YetkiId = user.YetkiId;
                ViewBag.UserId = user.Id;

            }

            TutorLessonCommentCO model = new TutorLessonCommentCO();
            var lesson = _tutorLessonService.GetByGuid(guid);
            if (lesson == null)
            {
                return View("Error");
            }
            var l = _lessonService.GetById(lesson.LessonId);
            if (l == null)
            {
                return View("Error");
            }
            var lessonComment = _lessonCommentService.GetByLessonComment(l.Guid);
            model.TutorLesson = lesson;
            model.Comment = lessonComment;

            TempData["lessonId"] = l.Id;

            return View(model);
        }

        //lesson guid geliyor
        [HttpGet]
        public ActionResult GetLesson2(Guid guid)
        {
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            if (userId > 0)
            {
                var user = _kullaniciService.GetById(userId);

                ViewBag.YetkiId = user.YetkiId;
                ViewBag.UserId = user.Id;
            }

            var lesson = _lessonService.GetByGuid(guid);
            if (lesson == null)
            {
                return View("Error");
            }
            TutorLessonCommentCO model = new TutorLessonCommentCO();
            var tutorLesson = _tutorLessonService.GetByTutorLesson(lesson.Guid);
            if (tutorLesson == null)
            {
                return View("Error");
            }
            var lessonComment = _lessonCommentService.GetByLessonComment(lesson.Guid);
            model.TutorLesson = tutorLesson;
            model.Comment = lessonComment;

            TempData["lessonId"] = lesson.Id;

            return View(model);
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
        public async Task<IActionResult> Create(LessonDTO model, IFormFile file)
        {
            var path = Path.Combine("Uploads", "DefaultPicture", "defaultLesson.png");
            if (file != null)
            {
                var webRoot = Path.Combine("Uploads", "Documents");
                path = Path.Combine(webRoot, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            model.Resim = path;
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.Guid = Guid.NewGuid();
            model.EklenmeZamani = DateTime.Now;
            model.Aktif = true;
            model.Silindi = false;

            var lessonId = _lessonService.Create(model);

            var tutorLesson = new TutorLessonDTO()
            {
                Guid = Guid.NewGuid(),
                Aktif = true,
                EklenmeZamani = DateTime.Now,
                KullaniciId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                    .SingleOrDefault()),
                LessonId = lessonId,
                Silindi = false,
                EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                    .SingleOrDefault())
            };
            _tutorLessonService.Create(tutorLesson);

            var user = _kullaniciService.GetById(Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .SingleOrDefault()));

            return RedirectToAction("GetTeacher", new RouteValueDictionary(new { controller = "Account", action = "GetTeacher", guid = user.Guid }));
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public ActionResult Edit(Guid guid)
        {
            var model = _lessonService.GetByGuid(guid);
            if (model == null)
            {
                return View("Error");
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER } })]
        public async Task<IActionResult> Edit(LessonDTO model, IFormFile file)
        {
            var lesson = _lessonService.GetById(Convert.ToInt32(TempData["lessonId"]));

            if (file != null)
            {
                var webRoot = Path.Combine("Uploads", "Documents");
                var path = Path.Combine(webRoot, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                model.Resim = path;
            }
            else
            {
                model.Resim = lesson.Resim;
            }

            model.Guid = lesson.Guid;
            model.Id = lesson.Id;
            model.Aktif = lesson.Aktif;
            model.Silindi = lesson.Silindi;
            model.EklenmeZamani = lesson.EklenmeZamani;
            model.EkleyenId = lesson.EkleyenId;

            _lessonService.Update(model);
            return RedirectToAction("Index");
        }

        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER, (int)Yetkiler.ADMIN } })]
        public ActionResult Delete(Guid guid)
        {
            var lesson = _lessonService.GetByGuid(guid);
            if (lesson == null)
            {
                return View("Error");
            }
            var tutorLesson = _tutorLessonService.GetByTutorLesson(lesson.Guid);

            if (tutorLesson != null)
            {
                _tutorLessonService.Delete(tutorLesson.Id);
            }
            _lessonService.Delete(lesson.Id);

            var user = _kullaniciService.GetById(Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .SingleOrDefault()));

            if (user.YetkiId == 1)
            {
                return RedirectToAction("GetLessons", "Admin");
            }

            return RedirectToAction("GetTeacher", new RouteValueDictionary(new { controller = "Account", action = "GetTeacher", guid = user.Guid }));
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetTutorLessons()
        {
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());

            var user = _kullaniciService.GetById(userId);

            var tutorLessons = _tutorLessonService.GetByLessons(user.Guid);

            return View(tutorLessons);
        }
    }
}