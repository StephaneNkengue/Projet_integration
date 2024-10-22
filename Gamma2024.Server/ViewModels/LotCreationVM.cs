using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Gamma2024.Server.ViewModels
{
    public class LotCreationVM
    {
        public string Numero { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double ValeurEstimeMin { get; set; }
        public double ValeurEstimeMax { get; set; }
        public double PrixOuverture { get; set; }
        public double? PrixMinPourVente { get; set; }
        public string Artiste { get; set; } = null!;
        public DateTime DateCreation { get; set; }
        public int IdCategorie { get; set; }
        public int IdVendeur { get; set; }
        public bool EstLivrable { get; set; }
        public double Hauteur { get; set; }
        public double Largeur { get; set; }
        public int IdMedium { get; set; }
        public int IdEncan { get; set; } // Pour associer le lot à un encan spécifique
        public List<string> PhotoUrls { get; set; } = new List<string>();
        public List<IFormFile> Photos { get; set; }
    }
}
