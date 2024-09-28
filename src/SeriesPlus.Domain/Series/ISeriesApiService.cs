using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeriesPlus.Series
{
    public interface ISeriesApiService
    {
        Task<SerieDto[]> GetSeriesAsync(string Titulo,string Genero);
    }
}
