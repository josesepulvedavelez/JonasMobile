using Microsoft.Maui.ApplicationModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonasMobile.Models
{
    public class Animal
    {
        [PrimaryKey]
        public int AnimalId { get; set; }
               
        public string NombreComun { get; set; } = string.Empty;
               
        public string NombreCientifico { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;
                
        public string Habitat { get; set; } = string.Empty;

        public string Alimentacion { get; set; } = string.Empty;

        public string Tipo { get; set; } = string.Empty;

        public string Tamano { get; set; } = string.Empty;

        public decimal PesoPromedio { get; set; }

        public string EstadoConservacion { get; set; } = string.Empty;

        public int CategoriaId { get; set; }
    }
}
