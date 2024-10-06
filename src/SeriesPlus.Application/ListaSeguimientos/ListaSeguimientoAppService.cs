using SeriesPlus.Series;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace SeriesPlus.ListaSeguimientos
{
    public class ListaSeguimientoAppService : ApplicationService, IListaSeguimientoAppService
    {
        private readonly IRepository<ListaSeguimiento, int> _watchListRepository; // acceso al repositorio de listas de seguimiento
        private readonly IRepository<Serie, int> _serieRepository; // acceso al repositorio de series

        // Constructor que recibe dos repositorios como parámetros
        public ListaSeguimientoAppService(IRepository<ListaSeguimiento, int> watchListRepository, IRepository<Serie, int> serieRepository)
        {
            _serieRepository = serieRepository;
            _watchListRepository = watchListRepository;
        }

        public async Task AddSerieAsync(int serieId) // Método para agregar una serie a la lista
        {
            var watchlist = ((List<ListaSeguimiento>)await _watchListRepository.GetListAsync()).FirstOrDefault();
                     //para obtener(Get) valores de la listSeg o saber si esta creada.
                         //definimos el atributo/variable listSeg.El firstOrdefault es para
                        //obtener el 1er elemento de la coleccion ya q para la verificacion
                        //q vamos a hacer no es necesario q este con todos los elementos
           
            if (watchlist == null)// si lista de seg.es null creo una lista.seg y la inserto en el repositorio(con Insert)
            {
                watchlist = new ListaSeguimiento();      // Inicializamos la colección de series para poder agregar elementos                                           
                watchlist.Series = new List<Serie>();
                await _watchListRepository.InsertAsync(watchlist);
            }

            var serie = await _serieRepository.GetAsync(serieId); // Obtenemos las series del reposit. de las series(con Get)
            watchlist.Series.Add(serie); // Agregamos la serie a la lista de seguimiento(add)
            await _watchListRepository.UpdateAsync(watchlist); // Actualizamos el repositorio de la lista de seguimiento(update)
        }
    }
}

