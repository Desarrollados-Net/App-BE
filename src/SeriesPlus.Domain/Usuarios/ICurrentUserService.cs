using System;

namespace SeriesPlus.Usuarios
{
    public interface ICurrentUserService
    {
        Guid? GetCurrentUserId();
    }
}