using SeriesPlus.Notificaciones;
using System;
using System.Threading.Tasks;

namespace SeriesPlus.Notificaciones
{
    public interface INotificacionService
    {
        Task CrearYEnviarNotificacionAsync(int usuarioId, string titulo, string mensaje, TipoNotificacion tipo);
    }
}