using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Shared.CriteriaObject;
using PrivateTutoringApplication.Shared.DTO;
using PrivateTutoringApplication.Shared.Enumerations;

namespace PrivateTutoringApplication.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly IGirisService _girisService;
        private readonly ITutorLessonService _tutorLessonService;
        private readonly ILessonService _lessonService;
        private IConfiguration Configuration { get; }

        public AccountController(IKullaniciService kullaniciService, IGirisService girisService, IConfiguration configuration, ITutorLessonService tutorLessonService, ILessonService lessonService)
        {
            Configuration = configuration;
            _kullaniciService = kullaniciService;
            _girisService = girisService;
            _tutorLessonService = tutorLessonService;
            _lessonService = lessonService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StudentRegister()
        {
            return View();
        }

        [HttpGet]
        public ActionResult TeacherRegister()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(GirisCO request)
        {
            KullaniciDTO user = _kullaniciService.GetByKullanici(request.Email, request.Sifre);

           if (user == null)
           {
               return View("Error");
           }

            var giris = new GirisDTO()
            {
                KullaniciId = user.Id,
                Durum = true,
                Aktif = true,
                Silindi = false
            };
            var girisId = _girisService.Create(giris);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);

                if (user.YetkiId == (int)Yetkiler.ADMIN)
                {
                    ViewBag.User = user;
                    return RedirectToAction("Index", "Admin");
                }

                if (user.YetkiId == (int)Yetkiler.TEACHER)
                {
                    ViewBag.User = user;
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.User = user;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> StudentRegister(StudentRegisterCO request, IFormFile file)
        {
            var controlEmail = _kullaniciService.GetByEmail(request.Email);
            if (controlEmail != null)
            {
                return View("Error");
            }

            var path = Path.Combine("Uploads", "DefaultPicture", "defaultMan.png");
            if (file != null)
            {
                var webRoot = Path.Combine("Uploads", "Documents");
                path = Path.Combine(webRoot, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var tuzlama = _kullaniciService.GetTuzlamaDegeri();
            var user = new KullaniciDTO()
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                Email = request.Email,
                KullaniciAdi = request.KullaniciAdi,
                TuzlamaDegeri = tuzlama,
                Sifre = _kullaniciService.Sifrele(request.Sifre, tuzlama),
                EklenmeZamani = DateTime.Now,
                Guid = Guid.NewGuid(),
                Silindi = false,
                Aktif = true,
                YetkiId = (int)Yetkiler.STUDENT,
                Resim = path
            };
            var userId = _kullaniciService.Create(user);

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> TeacherRegister(TeacherRegisterCO request, IFormFile file)
        {
            var controlEmail = _kullaniciService.GetByEmail(request.Email);
            if (controlEmail != null)
            {
                return View("Error");
            }

            var path = Path.Combine("Uploads", "DefaultPicture", "defaultMan.png");
            if (file != null)
            {
                var webRoot = Path.Combine("Uploads", "Documents");
                path = Path.Combine(webRoot, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            var tuzlama = _kullaniciService.GetTuzlamaDegeri();

            var user = new KullaniciDTO()
            {
                Ad = request.Ad,
                Soyad = request.Soyad,
                Email = request.Email,
                KullaniciAdi = request.KullaniciAdi,
                TuzlamaDegeri = tuzlama,
                Sifre = _kullaniciService.Sifrele(request.Sifre, tuzlama),
                EklenmeZamani = DateTime.Now,
                Guid = Guid.NewGuid(),
                Silindi = false,
                Aktif = false,
                YetkiId = (int)Yetkiler.TEACHER,
                Resim = path
            };
            var userId = _kullaniciService.Create(user);

            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetTeachers()
        {
            var teachers = _kullaniciService.GetByTeachers();
            return View(teachers);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetTeacher(Guid guid)
        {
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            if (userId > 0)
            {
                var user = _kullaniciService.GetById(userId);

                ViewBag.YetkiId = user.YetkiId;
                ViewBag.UserId = user.Id;

            }

            TeacherLessonCO model = new TeacherLessonCO();
            
            var teacher = _kullaniciService.GetByTeacher(guid);
            if (teacher == null)
            {
                return View("Error");
            }
            var lessons = _tutorLessonService.GetByLessons(guid);
            model.Kullanici = teacher;
            model.TutorLesson = lessons;
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Edit(Guid guid)
        {
            var user = _kullaniciService.GetByGuid(guid);
            if (user == null)
            {
                return View("Error");
            }
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(KullaniciDTO model, IFormFile file)
        {
            var user = _kullaniciService.GetById(Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name)
                .Select(c => c.Value)
                .SingleOrDefault()));

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
                model.Resim = user.Resim;
            }

            model.TuzlamaDegeri = user.TuzlamaDegeri;
            model.Sifre = user.Sifre;
            model.EklenmeZamani = user.EklenmeZamani;
            model.Guid = user.Guid;
            model.Aktif = user.Aktif;
            model.Silindi = user.Silindi;
            model.YetkiId = user.YetkiId;
            model.Id = user.Id;

            _kullaniciService.Update(model);

            return RedirectToAction("GetByUser", new RouteValueDictionary(new { controller = "Account", action = "GetByUser", Model = user }));
        }

        [Authorize]
        public IActionResult Delete(Guid guid)
        {
            var user = _kullaniciService.GetByGuid(guid);
            if (user == null)
            {
                return View("Error");
            }
            _kullaniciService.Delete(user.Id);

            var teacherLesson = _tutorLessonService.GetByLessons(guid);

            if (teacherLesson != null)
            {
                foreach (var item in teacherLesson)
                {
                    _tutorLessonService.Delete(item.Id);
                    _lessonService.Delete(item.LessonId);
                }
            }

            return RedirectToAction("GetTeachers", "Admin");
        }

        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.ADMIN } })]
        public IActionResult EditOnay(Guid guid)
        {
            var user = _kullaniciService.GetByGuid(guid);
            user.Aktif = true;
            _kullaniciService.Update(user);

            return RedirectToAction("GetTeachers", "Admin");
        }

        public ActionResult _headerPartialView()
        {
            var user = _kullaniciService.GetById(Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault()));
            if (user != null)
            {
                ViewBag.UserId = user.Id;
            }
            return PartialView();
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(YetkiKontrol), Arguments = new object[] { new int[] { (int)Yetkiler.TEACHER, (int)Yetkiler.STUDENT, (int)Yetkiler.ADMIN } })]
        public ActionResult GetByUser()
        {
            var userId = Convert.ToInt32(User.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value)
                .SingleOrDefault());
            var user = _kullaniciService.GetById(userId);
            if (user == null)
            {
                return View("Error");
            }

            return View(user);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}