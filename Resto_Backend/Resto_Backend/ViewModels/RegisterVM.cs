using System.ComponentModel.DataAnnotations;
namespace Resto_Backend.ViewModels
{
    public class RegisterVM
    {
        [Required, StringLength(200)]
        public string Fullname { get; set; }
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [Required, DataType(DataType.Password)]

        public string Password { get; set; }
        [Required, DataType(DataType.Password), Compare(nameof(Password))]

        public string CheckPassword { get; set; }
    }
}
