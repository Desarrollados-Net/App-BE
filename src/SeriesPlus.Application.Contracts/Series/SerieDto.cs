﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos; //agrgeggue (buscar plugin)

namespace SeriesPlus.Series
{
    public class SerieDto : EntityDto<int>
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

        public string ImdbID { get; set; }
        public int TotalTemporadas { get; set; }

        public ICollection<TemporadaDto> Temporadas { get; set; }
    }
}
