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
            var users = await _context.Users
                .Select(u => new {
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.Name,
                    u.FirstName,
                    u.Avatar,
                    Adresses = u.Adresses.Select(a => new { 
                        a.Id, 
                        a.Numero, 
                        a.Rue, 
                        a.Ville, 
                        a.CodePostal, 
                        a.Province, 
                        a.Pays 
                    }),
                    CarteCredits = u.CarteCredits.Select(c => new { 
                        c.Id, 
                        c.Numero,  // Changé de Number à Numero
                        ExpirationDate = $"{c.MoisExpiration:D2}/{c.AnneeExpiration}"  // Combiné MoisExpiration et AnneeExpiration
                    })
                })
                .ToListAsync();

            if (users == null || !users.Any())
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
