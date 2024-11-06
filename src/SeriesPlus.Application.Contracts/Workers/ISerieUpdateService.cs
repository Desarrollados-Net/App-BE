using System.Threading.Tasks;

namespace SeriesPlus.Series
{
    public interface ISerieUpdateService
    {
        Task VerificarYActualizarSeriesAsync();
    }
}