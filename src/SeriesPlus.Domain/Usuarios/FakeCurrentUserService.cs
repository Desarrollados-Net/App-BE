using SeriesPlus.Usuarios;
using System;

namespace SeriesPlus.EntityFrameworkCore
{
    public class FakeCurrentUserService : ICurrentUserService
    {
        public Guid? GetCurrentUserId()
        {
            return Guid.NewGuid();
        }
    }
}