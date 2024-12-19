using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace SeriesPlus.Series
{
    //como es una clase de Dom. tengo q heredarla de ABP
    public class Serie : AggregateRoot <int> //ponemos el id/key de la serie como int  
    {
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public string FechaLanzamiento { get; set; }
        public string Duracion { get; set; }
        public string FotoPortada { get; set; } // Considerar la ruta o el tipo de dato según la implementación de 'image'
        public string Idioma { get; set; }
        public string PaisOrigen { get; set; }
        public string CalificacionIMDB { get; set; } 
        public string Director { get; set; }
        public string Escritor { get; set; }
        public string Actores { get; set; }
        public int TotalTemporadas { get; set; }
       
        //public int idSerie { get; set; }

        public string ImdbID { get; set; }
        public ICollection<Temporada> Temporadas { get; set; }



        public Guid Creator { get; set; }
        public Guid CreatorId { get; set; }


        public ICollection<Review> Reviews { get; set; }

        public Serie()
        {
            Reviews = new List<Review>();
        }

        //public bool If_Emision { get; set; } // Para valores booleanos, como si está en emisión o no
    }
}
