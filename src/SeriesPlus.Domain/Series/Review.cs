using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.Domain.Entities;

namespace SeriesPlus.Series
{
    public class Review : AggregateRoot<int> //ponemos el id/key de la serie como int  
    {
        public byte nota { get; set; } //
        public string? comentario { get; set; }
        public DateTime fechaRealizado { get; set; }

        //Los ids del usuario que calificó y la serie calificada
        public int idUsuario { get; set; }
        public int idSerie { get; set; }


        //Los constructores vacios como con datos
        public Review()
        {
            nota = 0;
            comentario = string.Empty;
            fechaRealizado = DateTime.MinValue;
            idUsuario = 0;
            idSerie = 0;
        }

        public Review(byte nota, string? comentario, int idUsuario, int idSerie) 
        {
            this.nota = nota;
            this.comentario = comentario;
            fechaRealizado = DateTime.Now;
            this.idUsuario = idUsuario;
            this.idSerie = idSerie;
        }

    }
}