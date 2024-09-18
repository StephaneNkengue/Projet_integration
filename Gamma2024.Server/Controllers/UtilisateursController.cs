using Microsoft.AspNetCore.Mvc;

namespace Gamma2024.Server.Controllers
{
    public class UtilisateursController : Controller
    {
        //private readonly ApplicationDbContaxt _context;

        public UtilisateursController() //context
        {
            //context = _context;
        }

        public IActionResult Creer()
        {
            //recevoir les utilisateurs de mon formulaire recu
            //creer un utilisateur a partir de cet utilisateur recu 
            //enregistré cet user dans la bd
            //retourner un message ok

            return Ok();
        }
    }
}
