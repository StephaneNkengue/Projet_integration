using Gamma2024.Server.Models;

namespace Gamma2024.Server.Extensions
{
    public static class VendeurExtensions
    {
        public static IEnumerable<Vendeur> ToVendeur(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                var adresse = columns[2].Split(",");
                var numCiviqueRue = adresse[0].Split(" ", 2);

                yield return new Vendeur
                {
                    Nom = columns[0],
                    Prenom = columns[1],
                    Adresse = new Adresse
                    {
                        Numero = int.Parse(numCiviqueRue[0]),
                        Rue = numCiviqueRue[1],
                        Ville = adresse[1],
                        Province = adresse[2],
                        Pays = "Canada",
                        CodePostal = adresse[3],
                        EstDomicile = false
                    },
                    Telephone = columns[3],
                    Courriel = columns[4]
                };
            }
        }
    }
}
