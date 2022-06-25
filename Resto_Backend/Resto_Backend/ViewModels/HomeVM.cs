using Resto_Backend.Models;
using System.Collections;
using System.Collections.Generic;

namespace Resto_Backend.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Breakfast> Breakfast { get; set; }
    }
}
