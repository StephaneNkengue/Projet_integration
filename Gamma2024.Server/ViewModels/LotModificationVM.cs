using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Gamma2024.Server.ViewModels
{
    public class LotModificationVM : LotCreationVM
    {
        public List<IFormFile> NouvellesPhotos { get; set; }
        public List<int> PhotosASupprimer { get; set; }
    }
}
