using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    public class LessonCommentController : Controller
    {
        private readonly ILessonCommentService _lessonCommentService;
        private readonly ILessonService _lessonService;
        private IConfiguration Configuration { get; }

        public LessonCommentController(ILessonCommentService lessonCommentService, IConfiguration configuration, ILessonService lessonService)
        {
            _lessonCommentService = lessonCommentService;
            Configuration = configuration;
            _lessonService = lessonService;
        }

        [Authorize]
        [HttpGet]
        public ActionResult _CreateComment()
        {
            return PartialView();
        }

        [Authorize]
        [HttpPost]
        public ActionResult _CreateComment(LessonCommentDTO model)
        {
            model.EkleyenId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.Guid = Guid.NewGuid();
            model.EklenmeZamani = DateTime.Now;
            model.Aktif = true;
            model.Silindi = false;
            model.KullaniciId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            model.LessonId = Convert.ToInt32(TempData["lessonId"]);

            _lessonCommentService.Create(model);

            var lesson = _lessonService.GetById(model.LessonId);
            return RedirectToAction("GetLesson2", new RouteValueDictionary(new { controller = "Lesson", action = "GetLesson2", guid = lesson.Guid }));
        }

        [Authorize]
        public ActionResult Delete(Guid guid)
        {
            var model = _lessonCommentService.GetByGuid(guid);
            if (model == null)
            {
                return View("Error");
            }
            var lesson = _lessonService.GetById(model.LessonId);
            _lessonCommentService.Delete(model.Id);
            return RedirectToAction("GetLesson2", new RouteValueDictionary(new { controller = "Lesson", action = "GetLesson2", guid = lesson.Guid }));
        }
    }
}