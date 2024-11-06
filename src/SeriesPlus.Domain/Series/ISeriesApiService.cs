using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesPlus.Series
{
    public interface ISeriesApiService
    {
        Task<ICollection<SerieDto>> GetSeriesAsync(string Titulo);//string genero
        Task<SerieDto[]> BuscarSerieAsync(string titulo, string genero = null); // Genero es opcional
        Task<TemporadaDto> BuscarTemporadaAsync(string imdbId, int numeroTemporada); // Agregado para buscar temporada
    }
}
