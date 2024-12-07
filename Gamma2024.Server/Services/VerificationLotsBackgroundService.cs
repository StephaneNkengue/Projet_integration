using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gamma2024.Server.Data;
using Gamma2024.Server.Services;

namespace Gamma2024.Server.Services
{
    public class VerificationLotsBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<VerificationLotsBackgroundService> _logger;

        public VerificationLotsBackgroundService(
            IServiceProvider services,
            ILogger<VerificationLotsBackgroundService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _services.CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        var lotService = scope.ServiceProvider.GetRequiredService<LotService>();

                        var maintenant = DateTime.Now;

                        _logger.LogInformation($"Début de la vérification à {maintenant}");

                        var encansTermines = await context.Encans
                            .Where(e => !e.EstTermine && 
                                       e.DateFin < maintenant && 
                                       e.EncanLots.Any(el => el.Lot.Mise > 0))
                            .Select(e => new 
                            { 
                                e.Id,
                                e.NumeroEncan,
                                DerniereLotDate = e.EncanLots
                                    .Max(el => el.Lot.DateFinDecompteLot)
                            })
                            .Where(e => e.DerniereLotDate <= maintenant)
                            .ToListAsync(stoppingToken);

                        _logger.LogInformation($"Trouvé {encansTermines.Count} encans terminés à vérifier");
                        
                        foreach (var encan in encansTermines)
                        {
                            _logger.LogInformation($"Traitement de l'encan #{encan.NumeroEncan} (ID: {encan.Id})");
                            _logger.LogInformation($"Date du dernier lot : {encan.DerniereLotDate}");
                            
                            await lotService.VerifierEtFinaliserLotsExpires(encan.Id);
                        }
                    }

                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erreur lors de la vérification des lots");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }
    }
}
