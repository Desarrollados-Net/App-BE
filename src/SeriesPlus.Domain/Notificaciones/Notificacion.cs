﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesPlus.Notificaciones;
using Volo.Abp.Domain.Entities.Auditing;

namespace SeriesPlus.Domain.Notificaciones
{
    public class Notificacion : FullAuditedEntity<int>
    {
        public int UsuarioId { get; set; }
        public string Titulo { get; set; }
        public string Mensaje { get; set; }
        public bool Leida { get; set; }
        public TipoNotificacion Tipo { get; set; }
        public DateTime FechaCreacion { get; set; }

    }

}