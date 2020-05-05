using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PrivateTutoringApplication.IRepository;
using PrivateTutoringApplication.Repository;
using PrivateTutoringApplication.IService;
using PrivateTutoringApplication.Model.Entity;
using PrivateTutoringApplication.Model.Infrastructure;
using PrivateTutoringApplication.Presentation.Extensions;
using PrivateTutoringApplication.Presentation.Filters;
using PrivateTutoringApplication.Service;

namespace PrivateTutoringApplication.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = Int64.MaxValue; // In case of multipart
            });
            services.AddScoped<YetkiKontrol>();

            //services.AddTransient<IKullaniciRolService, KullaniciRolService>();
            services.AddDbContext<DatabaseContext>(x => x.UseNpgsql(Configuration.GetConnectionString("DivaConnection")));

            InitializeContainer(services);

            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login/";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Uploads"
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // kullanıcının kaç kere giriş yaptığını kontrol eden middware
            //app.UseRequestToken();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void InitializeContainer(IServiceCollection services)
        {
            services.AddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<YetkiKontrol>();

            services.AddTransient<IRepository<Kullanici>, Repository<Kullanici>>();
            services.AddTransient<IKullaniciRepository, KullaniciRepository>();
            services.AddTransient<IKullaniciService, KullaniciService>();

            services.AddTransient<IRepository<Lesson>, Repository<Lesson>>();
            services.AddTransient<ILessonRepository, LessonRepository>();
            services.AddTransient<ILessonService, LessonService>();

            services.AddTransient<IRepository<LessonComment>, Repository<LessonComment>>();
            services.AddTransient<ILessonCommentRepository, LessonCommentRepository>();
            services.AddTransient<ILessonCommentService, LessonCommentService>();

            services.AddTransient<IRepository<TutorLesson>, Repository<TutorLesson>>();
            services.AddTransient<ITutorLessonRepository, TutorLessonRepository>();
            services.AddTransient<ITutorLessonService, TutorLessonService>();

            services.AddTransient<IRepository<Schedule>, Repository<Schedule>>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddTransient<IScheduleService, ScheduleService>();

            services.AddTransient<IRepository<TutorSchedule>, Repository<TutorSchedule>>();
            services.AddTransient<ITutorScheduleRepository, TutorScheduleRepository>();
            services.AddTransient<ITutorScheduleService, TutorScheduleService>();

            services.AddTransient<IRepository<Yetki>, Repository<Yetki>>();
            services.AddTransient<IYetkiRepository, YetkiRepository>();
            services.AddTransient<IYetkiService, YetkiService>();

            services.AddScoped<IRepository<Giris>, Repository<Giris>>();
            services.AddScoped<IGirisRepository, GirisRepository>();
            services.AddScoped<IGirisService, GirisService>();
        }

        private static void SeedDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
                // Seed the database.
            }
        }
    }
}
