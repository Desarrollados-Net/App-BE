﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace SeriesPlus.Series
{
    public class TemporadaDto : EntityDto<int>
    {
        public string Titulo { get; set; }
        public DateOnly FechaLanzamiento { get; set; }
        public int NumeroTemporada { get; set; }

        //Foreign key
        public int SerieID { get; set; }

        //Relación uno a muchos con Episodio
        public ICollection<EpisodioDto> Episodios { get; set; }
    }
}