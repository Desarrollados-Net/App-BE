using System;
using System.Threading.Tasks;
using SeriesPlus.Application.Contracts.Notificaciones;
using SeriesPlus.Domain.Notificaciones;
using SeriesPlus.Notificaciones;
using SeriesPlus.Domain.Notificaciones;

namespace SeriesPlus.Application.Notificaciones
{
    public class EmailNotificador : INotificador
    {
        public bool PuedeEnviar(TipoNotificacion tipo)
        {
            return tipo == TipoNotificacion.Email;
        }

        public async Task EnviarNotificacionAsync(NotificacionDto notificacionDto)
        {
            // Convertir DTO a entidad de dominio
            var notificacion = new Notificacion
            {
                UsuarioId = notificacionDto.UsuarioId,
                Titulo = notificacionDto.Titulo,
                Mensaje = notificacionDto.Mensaje,
                Leida = false,
                Tipo = notificacionDto.Tipo,
            };

            // Lógica para enviar un correo electrónico usando notificacion.Titulo y notificacion.Mensaje
            // Simulación de envío de correo:
            await Task.Delay(1000); // Simula el tiempo de envío de un correo
            Console.WriteLine($"Email enviado a usuario {notificacion.UsuarioId}: {notificacion.Titulo} - {notificacion.Mensaje}");
        }
    }
}