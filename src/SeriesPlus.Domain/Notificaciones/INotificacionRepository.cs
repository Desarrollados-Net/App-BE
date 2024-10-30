using SeriesPlus.Domain.Notificaciones;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace SeriesPlus.Domain.Notificaciones
{
    public interface INotificacionRepository : IRepository<Notificacion, int>
    {
        Task<List<Notificacion>> GetNotificacionesNoLeidasAsync(int usuarioId);
    }
}