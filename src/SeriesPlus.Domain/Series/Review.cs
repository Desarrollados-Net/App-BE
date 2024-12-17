using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.Domain.Entities;

namespace SeriesPlus.Series
{
    public class Review : Entity<int> //ponemos el id/key de la serie como int  
    {
        public float calificacion {  get; set; }
        public string comentario { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Foreign key
        public int SerieID { get; set; }
        public Serie Serie { get; set; }
        
        // UsuarioId para rastrear el usuario que hizo la calificación
        public Guid UsuarioId { get; set; }
    }
}