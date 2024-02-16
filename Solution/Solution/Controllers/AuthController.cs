using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Solution.Models;
using Solution.Services.Repositories;
using System.Net;
using System.Security.Claims;

namespace Solution.Controllers
{
    public class AuthController(IUserRepository userRepository, ITokenRepository tokenRepository) : BaseController
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ITokenRepository _tokenRepository = tokenRepository;


        [HttpGet]
        public IActionResult RegisterAsync()
        {
            var model = new RegisterRequest();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterRequest formRequest)
        {
            if (!ModelState.IsValid || formRequest == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid request");
                return View(formRequest);
            }

            var user = await _userRepository.Register(formRequest.Email, formRequest.Password);

            if (user.StatusCode != HttpStatusCode.OK)
            {
                SetAlert(user.Message, AlertType.Danger);
                return View(formRequest);
            }

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            ViewData["returnUrl"] = returnUrl;
            var model = new LoginRequest();
            model.Email = "test@email.com";
            model.Password = "1SampaiDelapan!";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginRequest formRequest)
        {
            if (!ModelState.IsValid || formRequest == null)
            {
                SetAlert("Invalid Request", AlertType.Danger);
                return View(formRequest);
            }

            var user = await _userRepository.Login(formRequest.Email, formRequest.Password);

            if (user.StatusCode != HttpStatusCode.OK)
            {
                SetAlert(user.Message, AlertType.Danger);
                return View(formRequest);
            }

            var claims = _tokenRepository.CreateClaims(user.MasterUserModel.Id.ToString(), user?.MasterUserModel.Email);
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            string? returnUrl = HttpContext.Request.Query["returnUrl"];
            return Redirect(returnUrl ?? "/");
        }


        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
