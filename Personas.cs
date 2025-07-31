﻿using System.ComponentModel.DataAnnotations;

namespace Candidate
{
    public class Personas
    {
        [Key]
        public int IdPersona { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Cedula {  get; set; } = string.Empty;
        public string Provincia { get; set; } = string.Empty;   
    }
}
