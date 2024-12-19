using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SeriesPlus.Series
{
    public class ReviewDto
    {
        [Range(1, 5, ErrorMessage = "La calificación debe ser un valor entre 1 y 5")]
        public float calificacion { get; set; }
        public string comentario { get; set; }
        public DateTime FechaCreacion { get; set; }

        //Foreign key
        public int SerieID { get; set; }


        // UsuarioId para rastrear el usuario que hizo la calificación
        public Guid UsuarioId { get; set; }
    }
}