﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Cibertec.Mvc.Models
{
    public class RegisterUserViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        [StringLength(100,
            ErrorMessage ="El número de caracteres de {0} debe ser al menos de {2}",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirmar contraseña")]
        [Compare("Password",
            ErrorMessage ="La contraseña y la contraseña de confirmación no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}