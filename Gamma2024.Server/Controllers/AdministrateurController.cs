using Gamma2024.Server.Data;
using Gamma2024.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gamma2024.Server.Controllers
{
    [Authorize(Roles = ApplicationRoles.ADMINISTRATEUR)]
    public class AdministrateurController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public AdministrateurController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        //Liaison avec la carte de crédit manquantecarte de credit

        [HttpGet("ObtenirTousLesUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users
                .Include(Adresse => Adresse.Id)
                .ToListAsync();

            if (users == null)
            {
                return BadRequest("Liste d'utilisateur vide");
            }
            return Ok(users);
        }
    }
}
