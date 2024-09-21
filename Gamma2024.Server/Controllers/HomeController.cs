using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Gamma2024.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailOuPseudo) 
                       ?? await _userManager.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == model.EmailOuPseudo.ToLower());

            if (user == null)
            {
                return Unauthorized(new { message = "Utilisateur non trouvé" });
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName ?? string.Empty, model.Password, false, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new { message = "Connexion réussie", userId = user.Id, roles = roles });
            }
            else
            {
                return Unauthorized(new { message = "Échec de la connexion" });
            }
        }

        public class LoginModel
        {
            public string EmailOuPseudo { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}
