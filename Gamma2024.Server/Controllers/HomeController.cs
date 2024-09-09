using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            // Authentification réussie
            return Ok(new { message = "Connexion réussie" });
        }
        else
        {
            // Échec de l'authentification
            return Unauthorized(new { message = "Échec de la connexion" });
        }
    }
}

public class LoginModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}
