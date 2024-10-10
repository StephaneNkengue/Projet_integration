using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Controllers
{
    [Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
    [Route("api/[controller]")]
    public class AdministrateurController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdministrateurController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        //Liaison avec la carte de crédit manquante

        [HttpGet("ObtenirTousLesUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Adresses).Include(u => u.CarteCredits).ToListAsync();

            if (users == null)
            {
                return NotFound("Liste d'utilisateur vide");
            }
            return Ok(users);
        }


        [HttpGet("{membreId}")]
        public async Task<IActionResult> GetUserById(string membreId)
        {
            var user = await _context.Users.Include(u => u.Adresses).Include(u => u.CarteCredits).FirstOrDefaultAsync(x => x.Id == membreId);

            if (user == null)
            {
                return NotFound("Aucun utilisateur trouvé");
            }
            return Ok(user);
        }
    }
}
