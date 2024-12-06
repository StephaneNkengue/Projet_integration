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
                    await VerifierEncansTermines(stoppingToken);
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    _logger.LogError(ex, "Erreur lors de la vérification des lots");
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
        }

        private async Task VerifierEncansTermines(CancellationToken stoppingToken)
        {
            using var scope = _services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var lotService = scope.ServiceProvider.GetRequiredService<LotService>();

            var maintenant = DateTime.Now;

            var encansTermines = await dbContext.Encans
                .Where(e => !e.EstTermine && 
                           e.DateFin < maintenant && 
                           e.EncanLots.Any(el => el.Lot.Mise > 0))
                .Select(e => new 
                {
                    e.Id,
                    DerniereLotDate = e.EncanLots
                        .Select(el => el.Lot.DateFinDecompteLot)
                        .Max()
                })
                .Where(e => e.DerniereLotDate <= maintenant)
                .Select(e => e.Id)
                .ToListAsync(stoppingToken);

            _logger.LogInformation($"Trouvé {encansTermines.Count} encans terminés à vérifier");

            foreach (var idEncan in encansTermines)
            {
                try
                {
                    await lotService.VerifierEtFinaliserLotsExpires(idEncan);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Erreur lors du traitement de l'encan {idEncan}");
                    // Continue avec le prochain encan même si celui-ci échoue
                }
            }
        }
    }
}
