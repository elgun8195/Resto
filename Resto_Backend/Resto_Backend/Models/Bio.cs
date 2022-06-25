using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Resto_Backend.Models
{
    public class Bio
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Insta { get; set; }
        public string FaceBook { get; set; }
        public string Twitter { get; set; }
    }
}
