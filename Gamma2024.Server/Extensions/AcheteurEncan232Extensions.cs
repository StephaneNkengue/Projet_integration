using Gamma2024.Server.Extensions.Objets;
using Microsoft.IdentityModel.Tokens;

namespace Gamma2024.Server.Extensions
{
    public static class AcheteurEncan232Extensions
    {
        public static IEnumerable<AcheteurEncanInfo> GetAcheteursEncan232(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                if (!columns[1].IsNullOrEmpty())
                {
                    var livraison = columns[3] == "oui";
                    yield return new AcheteurEncanInfo
                    {
                        Pseudonyme = columns[0],
                        Lot = columns[1].Replace("lot #", ""),
                        PrixAchete = double.Parse(columns[2].Replace("$", "")),
                        Livraison = livraison,
                    };

                    if (!columns[5].IsNullOrEmpty())
                    {
                        livraison = columns[7] == "oui";
                        yield return new AcheteurEncanInfo
                        {
                            Pseudonyme = columns[0],
                            Lot = columns[5].Replace("lot #", ""),
                            PrixAchete = double.Parse(columns[6].Replace("$", "")),
                            Livraison = livraison,
                        };

                        if (!columns[9].IsNullOrEmpty())
                        {
                            livraison = columns[11] == "oui";
                            yield return new AcheteurEncanInfo
                            {
                                Pseudonyme = columns[0],
                                Lot = columns[9].Replace("lot #", ""),
                                PrixAchete = double.Parse(columns[10].Replace("$", "")),
                                Livraison = livraison,
                            };
                        }
                    }
                }
            }
        }
    }
}
