using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesPlus.Series
{
    public class CreateUpdateSerieDto
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Duracion { get; set; }
        public string FotoPortada { get; set; } // Considerar la ruta o el tipo de dato según la implementación de 'image'
        public string Idioma { get; set; }
        public string PaisOrigen { get; set; }
        public string CalificacionIMBD { get; set; }
        public string Director { get; set; }
        public string Escritor { get; set; }
        public string Actores { get; set; }
    }
}
