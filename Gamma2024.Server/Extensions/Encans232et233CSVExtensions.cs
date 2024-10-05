using Gamma2024.Server.Models;

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

        public static IEnumerable<Lot> ToLot(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(';');

                if (!string.IsNullOrEmpty(columns[1]))
                {
                    var livrable = false;
                    if (columns[11] == "oui")
                    {
                        livrable = true;
                    }

                    yield return new Lot
                    {
                        Numero = columns[1],
                        //Prix ouverture
                        //val min pour vente
                        ValeurEstimeMin = double.Parse(columns[4].Replace("$", "").Trim()),
                        ValeurEstimeMax = double.Parse(columns[5].Replace("$", "").Trim()),
                        Categorie = new Categorie
                        {
                            Nom = columns[6]
                        },
                        Artiste = columns[7],
                        Dimensions = columns[8],
                        Description = columns[9],
                        Medium = new Medium
                        {
                            Type = columns[10]
                        },
                        estLivrable = livrable
                    };
                }

            }
        }
    }
}
