using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SeriesPlus.Series
{
    //como es una clase de Dom. tengo q heredarla de ABP
    public class Serie : AggregateRoot <int> //ponemos el id/key de la serie como int  
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
     
       //public bool If_Emision { get; set; } // Para valores booleanos, como si está en emisión o no
    }
}
