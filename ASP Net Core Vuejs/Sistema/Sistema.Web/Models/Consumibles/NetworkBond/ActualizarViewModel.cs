﻿using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Consumibles.NetworkBond
{
    public class ActualizarViewModel
    {
        [Required]
        public int idnw { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre de Network Bond.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nwbond { get; set; }
    }
}
