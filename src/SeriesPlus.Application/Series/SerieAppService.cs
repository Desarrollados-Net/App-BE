using Microsoft.AspNetCore.Authorization;
using SeriesPlus.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace SeriesPlus.Series
{
    [Authorize]
    public class SerieAppService : CrudAppService <Serie,SerieDto, int, PagedAndSortedResultRequestDto, CreateUpdateSerieDto, CreateUpdateSerieDto>, ISerieAppService
    {
        private readonly ISeriesApiService _seriesService;
        private readonly IRepository<Serie, int> _serieRepository;
        private readonly ICurrentUserService _currentUserService;


        public SerieAppService(IRepository<Serie, int> repository, ISeriesApiService seriesService, ICurrentUserService UserService) : base(repository)
        {
            _seriesService = seriesService;
            _serieRepository = repository;
            _currentUserService = UserService;
        }
        public async Task<ICollection<SerieDto>> SearchAsync(string Titulo)//Genero
        {
            return await _seriesService.GetSeriesAsync(Titulo);//Genero
        }
        public async Task<SerieDto[]> BuscarSerieAsync(string titulo, string genero = null)
        {
            return await _seriesService.BuscarSerieAsync(titulo, genero);
        }

        // Nuevo método para buscar temporadas
        public async Task<TemporadaDto> BuscarTemporadaAsync(string imdbId, int numeroTemporada)
        {
            return await _seriesService.BuscarTemporadaAsync(imdbId, numeroTemporada);
        }


        public async Task CalificarSerieAsync(ReviewDto input)
        {
            // Obtener la serie del repositorio
            var serie = await _serieRepository.GetAsync(input.SerieID);
            if (serie == null)
            {
                throw new EntityNotFoundException(typeof(Serie), input.SerieID);
            }

            // Obtener el ID del usuario actual
            var userIdActual = _currentUserService.GetCurrentUserId();
            if (!userIdActual.HasValue)
            {
                throw new InvalidOperationException("User ID cannot be null");
            }

            // Un usuario solo puede calificar las series relacionadas a él
            if (serie.CreatorId != userIdActual.Value)
            {
                throw new UnauthorizedAccessException("No puedes calificar esta serie.");
            }

            // Un usuario no puede calificar 2 veces la misma serie
            var calificacionExistente = serie.Reviews.FirstOrDefault(c => c.UsuarioId == userIdActual.Value);
            if (calificacionExistente != null)
            {
                throw new InvalidOperationException("Ya has calificado esta serie.");
            }

            // Crear la nueva calificación
            var review = new Review
            {
                calificacion = input.calificacion,
                comentario = input.comentario,
                FechaCreacion = DateTime.Now, // Asegúrate de asignar la fecha de creación
                SerieID = input.SerieID,
                UsuarioId = userIdActual.Value // Asigna el ID del usuario actual
            };

            // Agregar la calificación a la serie
            serie.Reviews.Add(review);

            // Actualizar la serie en el repositorio
            await _serieRepository.UpdateAsync(serie);
        }
    }
}
