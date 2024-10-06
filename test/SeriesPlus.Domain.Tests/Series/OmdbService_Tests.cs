using SeriesPlus.Series;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Xunit;

namespace SeriesPlus.Series
{   //abstract
    public abstract class OmdbService_Tests<TStartupModule> : SeriesPlusDomainTestBase<TStartupModule> where TStartupModule : IAbpModule
    {
        private readonly OmdbService _service;//pongo el nombre de la entidad a probar

        public OmdbService_Tests()
        {
            _service = new OmdbService();//como solo vamos a probar el objeto Omdbservice no tiene sentido probar la interfaz
        }                                //nos interesa probar solo este objeto de servicio omdb

        //Test 1 fciona bien
        [Fact] //este es el metodo del test q se identifica con el atributo q dice Fast.
        public async Task Should_Search_One_Serie() //los metodos de test de abp usa Should_"algo"
        {
            //Arrange  = Configuramos el test , esdecir definimos las variables q vamos a utilizar en el test
            var Titulo = "Game of Thrones";

            //Act = con esto ejecuto el test y luego de la ejecucion entra a probar OmbdService y devuelve un result
            var result = await _service.GetSeriesAsync(Titulo);//, String.Empty

            //Assert = con estas lineas verifico cada resultado de la lista result
            result.Count.ShouldBeGreaterThan(0);         //si el n° de registros de result no es mayor q 0 entonces falla el test(no pasa la prueba)
            result.ShouldContain(b => b.Titulo == Titulo);       //la otra prueba es q me devuelva el valor de Titulo(el cual es Game of Thrones)
        }

        //Test 2 q falla
        [Fact] 
        public async Task Should_Search_None_Serie()
        {
            //Arrange
            var Titulo = "ufyffflflffñfñififiy";

            //Act 
            var result = await _service.GetSeriesAsync(Titulo);//, String.Empty por el genero

            //Assert
            result.Count.ShouldBe(0);//devuelve 0 (lo correcto xq se esta buscando una serie con caracteres random)
        }
    }
}
