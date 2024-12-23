﻿using SeriesPlus.Application.Contracts.Notificaciones;
using SeriesPlus.Notificaciones;
using System.Threading.Tasks;

public class NotificadorPantalla : INotificador
{
    public bool PuedeEnviar(TipoNotificacion tipo) => tipo == TipoNotificacion.Pantalla;

    public Task EnviarNotificacionAsync(NotificacionDto notificacion)
    {
        // Lógica para mostrar la notificación en pantalla
        return Task.CompletedTask;
    }
}