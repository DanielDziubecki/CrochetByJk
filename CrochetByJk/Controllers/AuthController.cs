using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrochetByJk.Messaging.Core;
using CrochetByJk.Messaging.Queries;
using CrochetByJk.Model.Model;
using CrochetByJk.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace CrochetByJk.Controllers
{
    //todo: przeniesc logowanie do handlerów
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ICqrsBus bus;
        private readonly UserManager<ApplicationUser> userManager;
       
        public AuthController(ICqrsBus bus)
        {
            this.bus = bus;
            userManager = bus.RunQueryAsync<UserManager<ApplicationUser>>(new GetApplicationUserManagerQuery()).Result;

        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            var model = new UserLoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LogIn(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = await userManager.FindAsync(model.Email, model.Password);
            if (user != null)
            {
                var identity = await userManager.CreateIdentityAsync(
                    user, DefaultAuthenticationTypes.ApplicationCookie);

                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignIn(identity);

                return Redirect(GetRedirectUrl(model.ReturnUrl));
            }
            ModelState.AddModelError("", "Nieprawidłowy email lub hasło");
            return View();
        }

        public ActionResult LogOut()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new ApplicationUser()
            {
                UserName = model.Email,
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await SignIn(user);
                return RedirectToAction("index", "home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                return Url.Action("Index", "Home");

            return returnUrl;
        }

        private async Task SignIn(ApplicationUser user)
        {
            var identity = await userManager.CreateIdentityAsync(
                user, DefaultAuthenticationTypes.ApplicationCookie);

            GetAuthenticationManager().SignIn(identity);
        }
        private IAuthenticationManager GetAuthenticationManager()
        {
            var ctx = Request.GetOwinContext();
            return ctx.Authentication;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                userManager?.Dispose();

            base.Dispose(disposing);
        }

      
    }
}