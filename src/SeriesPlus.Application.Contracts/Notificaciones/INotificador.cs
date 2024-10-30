using SeriesPlus.Notificaciones;
using System.Threading.Tasks;

namespace SeriesPlus.Application.Contracts.Notificaciones
{
    public interface INotificador
    {
        bool PuedeEnviar(TipoNotificacion tipo);
        Task EnviarNotificacionAsync(NotificacionDto notificacion);
    }
}