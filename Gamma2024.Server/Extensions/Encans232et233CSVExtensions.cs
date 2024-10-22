using Gamma2024.Server.Models;
using Microsoft.IdentityModel.Tokens;

namespace Gamma2024.Server.Extensions
{
    public static class Encans232et233CSVExtensions
    {
        public static IEnumerable<Categorie> GetCategories(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                if (!string.IsNullOrEmpty(columns[1]))
                {
                    yield return new Categorie
                    {
                        Nom = columns[6]
                    };
                }

            }
        }

        public static IEnumerable<Medium> GetMediums(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                if (!string.IsNullOrEmpty(columns[1]))
                {
                    yield return new Medium
                    {
                        Type = columns[10]
                    };
                }

            }
        }

        public static IEnumerable<Lot> ToLotParEncan(this IEnumerable<string> source, int numeroEncan)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                if (!string.IsNullOrEmpty(columns[1]) && int.Parse(columns[0]) == numeroEncan)
                {
                    var livrable = false;
                    if (columns[11] == "oui")
                    {
                        livrable = true;
                    }

                    var prixMinVente = 0.00;
                    if (!columns[3].IsNullOrEmpty())
                    {
                        prixMinVente = double.Parse(columns[3].Replace("$", "").Trim());
                    }

                    var dimensions = columns[8].Split("x");

                    yield return new Lot
                    {
                        Numero = columns[1],
                        PrixOuverture = double.Parse(columns[2].Replace("$", "").Trim()),
                        PrixMinPourVente = prixMinVente,
                        ValeurEstimeMin = double.Parse(columns[4].Replace("$", "").Trim()),
                        ValeurEstimeMax = double.Parse(columns[5].Replace("$", "").Trim()),
                        Categorie = new Categorie
                        {
                            Nom = columns[6]
                        },
                        Artiste = columns[7],
                        Hauteur = double.Parse(dimensions[0].Trim()),
                        Largeur = double.Parse(dimensions[1].Trim()),
                        Description = columns[9],
                        Medium = new Medium
                        {
                            Type = columns[10]
                        },
                        EstLivrable = livrable
                    };
                }

            }
        }

        public static IEnumerable<IEnumerable<string>> GetImagesParLotParEncan(this IEnumerable<string> source, int numeroEncan)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                if (!string.IsNullOrEmpty(columns[1]) && int.Parse(columns[0]) == numeroEncan)
                {
                    var images = new List<string>();

                    images.Add(columns[12]);
                    images.Add(columns[13]);
                    images.Add(columns[14]);

                    yield return images;
                }

            }
        }
    }
}
