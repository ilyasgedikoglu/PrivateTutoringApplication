using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Presentation.Models;
using PrivateTutoringApplication.Shared.CriteriaObject;
using PrivateTutoringApplication.Shared.DTO;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly ITutorLessonService _tutorLessonService;
        private readonly ILessonService _lessonService;
        private IConfiguration Configuration { get; }

        public HomeController(IKullaniciService kullaniciService, ITutorLessonService tutorLessonService, IConfiguration configuration, ILessonService lessonService)
        {
            Configuration = configuration;
            _kullaniciService = kullaniciService;
            _tutorLessonService = tutorLessonService;
            _lessonService = lessonService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var lessons = _lessonService.GetLessons().OrderByDescending(x => x.EklenmeZamani).Take(6);
            var teachers = _kullaniciService.GetByTeachers().OrderByDescending(x => x.EklenmeZamani).Take(3);
            HomeIndexCO model = new HomeIndexCO();
            model.Kullanici = teachers;
            model.Lesson = lessons;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();
        }

        //ad, soyad, kategori ve ders adına göre ama yapar
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SearchKey(SearchDTO model)
        {
            var lessons = _tutorLessonService.Search(model.SearchKey.ToLower());
            return View(lessons);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
