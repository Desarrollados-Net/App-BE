﻿using SeriesPlus.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace SeriesPlus.Series
{
    public class Episodio : Entity<int>
    {
        public int NumeroEpisodio { get; set; }
        public DateOnly FechaEstreno { get; set; }
        public string Titulo { get; set; }
        public string Directores { get; set; }
        public string Escritores { get; set; }
        public string Duracion { get; set; }
        public string Resumen { get; set; }

        //Foreign key
        public int TemporadaID { get; set; }
        public Temporada Temporada { get; set; }
    }
}