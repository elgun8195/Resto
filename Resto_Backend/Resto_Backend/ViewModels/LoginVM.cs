﻿using System.ComponentModel.DataAnnotations;

namespace Resto_Backend.ViewModels
{
    public class LoginVM
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
