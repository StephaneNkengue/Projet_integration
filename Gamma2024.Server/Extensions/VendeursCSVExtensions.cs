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

                var adresse = columns[2].Split(",").Select(a => a.Trim()).ToList();

                var numCiviqueRue = adresse[0].Split(" ", 2).Select(a => a.Trim()).ToList();

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
                        CodePostal = adresse[3].Replace(" ", ""),
                        EstDomicile = false
                    },
                    Telephone = columns[3],
                    Courriel = columns[4]
                };
            }
        }

        public static IEnumerable<IEnumerable<string>> GetNumeroLotsEncan232(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                var lots = columns[5]
                    .Split(",")
                    .Select(a => a.Trim().Replace("lot #", ""))
                    .ToList();

                yield return lots;
            }
        }

        public static IEnumerable<IEnumerable<string>> GetNumeroLotsEncan233(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                var lots = columns[6]
                    .Split(",")
                    .Select(a => a.Trim().Replace("lot #", ""))
                    .ToList();

                yield return lots;
            }
        }
    }
}

