﻿
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using SeriesPlus.Series;
using SeriesPlus.Notificaciones;
using System.Collections.Generic;

namespace SeriesPlus.Series
{
    public class SerieUpdateService : DomainService, ISerieUpdateService // Implementa la interfaz
    {
        private readonly ISeriesApiService _seriesApiService;
        private readonly IRepository<Serie, int> _serieRepository;
        private readonly INotificacionService _notificacionService;

        public SerieUpdateService(
            ISeriesApiService seriesApiService,
            IRepository<Serie, int> serieRepository,
            INotificacionService notificacionService) // Inyección del servicio de notificaciones
        {
            _seriesApiService = seriesApiService;
            _serieRepository = serieRepository;
            _notificacionService = notificacionService; // Asignación del servicio
        }

        public async Task VerificarYActualizarSeriesAsync()
        {
            var series = await _serieRepository.GetListAsync();

            foreach (var serie in series)
            {
                var apiSeries = await _seriesApiService.BuscarSerieAsync(serie.Titulo, serie.Genero);

                if (apiSeries != null && apiSeries.Length > 0)
                {
                    var apiSerie = apiSeries.FirstOrDefault();

                    // Si la serie tiene más temporadas, se agrega la nueva temporada
                    if (apiSerie.TotalTemporadas > serie.TotalTemporadas)
                    {
                        var nuevaTemporadaNumero = serie.TotalTemporadas + 1;
                        var nuevaTemporadaApi = await _seriesApiService.BuscarTemporadaAsync(apiSerie.ImdbID, nuevaTemporadaNumero);

                        if (nuevaTemporadaApi != null)
                        {
                            var nuevaTemporada = new Temporada
                            {
                                NumeroTemporada = nuevaTemporadaNumero,
                                Episodios = nuevaTemporadaApi.Episodios.Select(e => new Episodio
                                {
                                    Titulo = e.Titulo,
                                    NumeroEpisodio = e.NumeroEpisodio,
                                    FechaEstreno = e.FechaEstreno
                                }).ToList()
                            };

                            serie.Temporadas.Add(nuevaTemporada);
                            serie.TotalTemporadas = apiSerie.TotalTemporadas;

                            await _serieRepository.UpdateAsync(serie);

                            // Notificar al usuario sobre la nueva temporada
                            var tituloNotificacionTemporada = $"Nueva temporada disponible de {serie.Titulo}";
                            var mensajeNotificacionTemporada = $"La temporada {nuevaTemporadaNumero} ya está disponible en {serie.Titulo}.";

                            var usuarioId = 001; // Suponiendo un usuario por defecto

                            // Los usuarios pueden elegir si quieren notificaciones por mail o pantalla
                            await _notificacionService.CrearYEnviarNotificacionAsync(
                                usuarioId, tituloNotificacionTemporada, mensajeNotificacionTemporada, TipoNotificacion.Email);
                            await _notificacionService.CrearYEnviarNotificacionAsync(
                                usuarioId, tituloNotificacionTemporada, mensajeNotificacionTemporada, TipoNotificacion.Pantalla);
                        }
                    }

                    // Obtener la última temporada local
                    var ultimaTemporadaLocal = serie.Temporadas.OrderByDescending(t => t.NumeroTemporada).FirstOrDefault();
                    if (ultimaTemporadaLocal != null)
                    {
                        // Obtener la última temporada desde la API
                        var apiUltimaTemporada = await _seriesApiService.BuscarTemporadaAsync(apiSerie.ImdbID, ultimaTemporadaLocal.NumeroTemporada);

                        if (apiUltimaTemporada != null)
                        {
                            // Comparar la cantidad de episodios
                            if (apiUltimaTemporada.Episodios.Count > ultimaTemporadaLocal.Episodios.Count)
                            {
                                // Detectar episodios nuevos
                                var episodiosLocales = ultimaTemporadaLocal.Episodios.Select(e => e.NumeroEpisodio).ToHashSet();
                                var episodiosNuevos = apiUltimaTemporada.Episodios
                                    .Where(e => !episodiosLocales.Contains(e.NumeroEpisodio))
                                    .ToList();

                                if (episodiosNuevos.Any())
                                {
                                    // Lógica para manejar los episodios nuevos
                                    foreach (var episodioNuevo in episodiosNuevos)
                                    {
                                        var nuevoEpisodio = new Episodio
                                        {
                                            Titulo = episodioNuevo.Titulo,
                                            NumeroEpisodio = episodioNuevo.NumeroEpisodio,
                                            FechaEstreno = episodioNuevo.FechaEstreno,
                                            TemporadaID = ultimaTemporadaLocal.Id
                                        };

                                        // Agregar a la colección de episodios de la temporada local
                                        ultimaTemporadaLocal.Episodios.Add(nuevoEpisodio);
                                    }

                                    // Reemplazo de la última temporada
                                    var ultimaTemporadaSerie = serie.Temporadas.OrderByDescending(t => t.NumeroTemporada).FirstOrDefault();
                                    var listaTemporadas = serie.Temporadas.ToList();
                                    var indiceUltimaTemporadaSerie = listaTemporadas.IndexOf(ultimaTemporadaSerie);
                                    listaTemporadas[indiceUltimaTemporadaSerie] = ultimaTemporadaLocal;
                                    serie.Temporadas = listaTemporadas;

                                    await _serieRepository.UpdateAsync(serie);

                                    // Generar y persistir la notificación para la serie
                                    var tituloNotificacion = $"Nuevos episodios en {serie.Titulo}";
                                    var mensajeNotificacion = $"Se han añadido {episodiosNuevos.Count} nuevos episodios en la serie {serie.Titulo}.";

                                    var usuarioId = 001; // Suponiendo un usuario por defecto

                                    // Notificar al usuario sobre los nuevos episodios
                                    await _notificacionService.CrearYEnviarNotificacionAsync(
                                        usuarioId, tituloNotificacion, mensajeNotificacion, TipoNotificacion.Email);
                                    await _notificacionService.CrearYEnviarNotificacionAsync(
                                        usuarioId, tituloNotificacion, mensajeNotificacion, TipoNotificacion.Pantalla);
                                }
                            }
                        }
                    }
                }
            }
        }


        // Método para persistir series en la base de datos
        public async Task PersistirSeriesAsync(SerieDto[] seriesDto)
        {
            var seriesExistentes = await _serieRepository.GetListAsync(); // Obtener todas las series

            foreach (var serieDto in seriesDto)
            {
                // Comprobación para evitar excepciones al acceder a propiedades de un objeto que podría ser null
                if (serieDto == null) continue; // Salta si serieDto es null

                var serieExistente = seriesExistentes.FirstOrDefault(s => s.ImdbID == serieDto.ImdbID);

                if (serieExistente == null)
                {
                    // Crear nueva serie
                    var nuevaSerie = new Serie
                    {
                        Titulo = serieDto.Titulo,
                        Genero = serieDto.Genero,
                        FechaLanzamiento = serieDto.FechaLanzamiento,
                        Duracion = serieDto.Duracion,
                        FotoPortada = serieDto.FotoPortada,
                        Idioma = serieDto.Idioma,
                        PaisOrigen = serieDto.PaisOrigen,
                        CalificacionIMDB = serieDto.CalificacionIMDB,
                        Director = serieDto.Director,
                        Escritor = serieDto.Escritor,
                        Actores = serieDto.Actores,
                        ImdbID = serieDto.ImdbID,
                        TotalTemporadas = serieDto.TotalTemporadas,
                        Temporadas = new List<Temporada>() // Asegúrate de inicializar la colección
                    };

                    if (serieDto.Temporadas != null)
                    {
                        foreach (var temporadaDto in serieDto.Temporadas)
                        {
                            var nuevaTemporada = new Temporada
                            {
                                NumeroTemporada = temporadaDto.NumeroTemporada,
                                Titulo = temporadaDto.Titulo,
                                FechaLanzamiento = temporadaDto.FechaLanzamiento,
                                Episodios = temporadaDto.Episodios.Select(e => new Episodio
                                {
                                    NumeroEpisodio = e.NumeroEpisodio,
                                    Titulo = e.Titulo,
                                    FechaEstreno = e.FechaEstreno
                                }).ToList()
                            };
                            nuevaSerie.Temporadas.Add(nuevaTemporada);
                        }
                    }

                    // Persistir la nueva serie en la base de datos
                    await _serieRepository.InsertAsync(nuevaSerie);
                }
                else
                {
                    // Actualizar la serie existente con nueva información
                    serieExistente.TotalTemporadas = serieDto.TotalTemporadas;
                    await _serieRepository.UpdateAsync(serieExistente);
                }
            }
        }
    }
}