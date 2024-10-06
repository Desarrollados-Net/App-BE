using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SeriesPlus.ListaSeguimientos
{
    public interface IListaSeguimientoAppService : IApplicationService
    {
        Task AddSerieAsync(int serieId);//definimos una operacion de sistema q va a estar relacionada con una Entidad serie
    }                                   //aqui definio el comportamiento del metodo para agregar una serie a la listaSeg. 
}                                       //tamos implementando esta lista de seg sin hacer referencia al usuario(mas adelante)
                                        //haremos referencia al usuario
                                        //Task se usa cuando hacemos una ejecucion Asincronica (con Async),significa q es una 
                                       // Tarea asincronia.Y aqui no devolvemos nada(gralmente task no devuelve nada)

