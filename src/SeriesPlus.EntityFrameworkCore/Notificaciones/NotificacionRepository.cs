using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeriesPlus.Domain.Notificaciones;
using SeriesPlus.Domain.Notificaciones;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace SeriesPlus.EntityFrameworkCore.Notificaciones
{
    public class NotificacionRepository : EfCoreRepository<SeriesPlusDbContext, Notificacion, int>, INotificacionRepository
    {
        public NotificacionRepository(IDbContextProvider<SeriesPlusDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Notificacion>> GetNotificacionesNoLeidasAsync(int usuarioId)
        {
            return await DbSet
                .Where(n => n.UsuarioId == usuarioId && !n.Leida)
                .ToListAsync();
        }
    }
}