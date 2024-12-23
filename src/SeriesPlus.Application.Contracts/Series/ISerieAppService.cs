﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SeriesPlus.Series
{
    public interface ISerieAppService : ICrudAppService<SerieDto,int,PagedAndSortedResultRequestDto ,CreateUpdateSerieDto, CreateUpdateSerieDto> 
    {
        Task<ICollection<SerieDto>> SearchAsync(string? Titulo); //El ? al final de string permite que el valor sea null
                                                                 //, string? Genero
        Task<SerieDto[]> BuscarSerieAsync(string titulo, string genero = null); // Hacer que el genero sea opcional
        Task<TemporadaDto> BuscarTemporadaAsync(string imdbId, int numeroTemporada);
        Task CalificarSerieAsync(ReviewDto input);

    }                                                     
}
