using Gamma2024.Server.Data;

namespace Gamma2024.Server.Services
{
    public class FactureService
    {
        private readonly ApplicationDbContext _context;

        public FactureService(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
