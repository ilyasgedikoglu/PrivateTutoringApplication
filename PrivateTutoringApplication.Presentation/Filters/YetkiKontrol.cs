using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PrivateTutoringApplication.IService;

namespace PrivateTutoringApplication.Presentation.Filters
{
    public class YetkiKontrol : ActionFilterAttribute
    {
        private readonly IKullaniciService _kullaniciService;
        private int[] _yetkiler;


        public YetkiKontrol()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kullaniciService"></param>
        /// <param name="yetki"></param>
        public YetkiKontrol(IKullaniciService kullaniciService, int[] yetkiler)
        {
            _yetkiler = yetkiler;
            this._kullaniciService = kullaniciService;
            //if (this._kullaniciRolService == null)
            //    this._kullaniciRolService = new KullaniciRolService(new KullaniciRolRepository(new Repository<KullaniciRol>(),new Repository<RolYetki>(), new Repository<Yetki>()));

        }


        /// <summary>
        /// 
        /// </summary>
        private int Yetki { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //To do : before the action executes  

            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                // context.Result = new UnauthorizedResult();
                context.Result = new UnauthorizedResult();
                return;
            }



            var id = Convert.ToInt32(context.HttpContext.User.Identity.Name);

            //buraya bak
            bool isAuthorized = _kullaniciService.KullaniciYetkiKontrol(id, _yetkiler);

            if (!isAuthorized)
            {
                context.Result = new JsonResult(new { Forbidden = HttpStatusCode.Forbidden, mesaj = "Yetkiniz bulunmamaktadır!" });
                //  context.Result = new UnauthorizedResult();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //To do : after the action executes  
        }
    }
}
