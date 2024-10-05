using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeriesPlus.Series;
using Volo.Abp.DependencyInjection;

namespace SeriesPlus.Series
{

    public class OmdbService : ISeriesApiService
    {
        private static readonly string apiKey = "39e61792"; // Reemplaza con tu clave API de OMDb.
        private static readonly string baseUrl = "http://www.omdbapi.com/";

        public async Task<ICollection<SerieDto>> GetSeriesAsync(string Titulo, string Genero)
        {
            using HttpClient client = new HttpClient();
            List<SerieDto> series = new List<SerieDto>();

            string url = $"{baseUrl}?s={Titulo}&apikey={apiKey}&type=series";//tittle

            try
            {
                // Hacer la solicitud HTTP y obtener la respuesta como string
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a un objeto SearchResponse
                var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(jsonResponse);

                // Retornar la lista de series si existen 
                //searchResponse es una lista de series proveniente de la api y se la pasamos a una variable nueva serieOmbd

                var seriesOmdb = searchResponse?.Search ?? new List<SerieOmdb>();
                foreach (var serieOmdb in seriesOmdb)
                {                               //de la lista(serieOmbd), por cada serieOmbd[1,2,3,4], creamos un SerieDto
                    series.Add(new SerieDto     //a el cual le vamos asignar una serie de esa lista.
                    {
                        Titulo = serieOmdb.Titulo,
                        FechaLanzamiento = serieOmdb.FechaLanzamiento,
                        Director = serieOmdb.Director,
                        Escritor = serieOmdb.Escritor,
                        Actores = serieOmdb.Actores,
                        FotoPortada = serieOmdb.FotoPortada,
                        PaisOrigen = serieOmdb.PaisOrigen,
                        CalificacionIMBD = serieOmdb.CalificacionIMBD,
                        Duracion = serieOmdb.Duracion,
                        Genero = serieOmdb.Genero,
                        Idioma = serieOmdb.Idioma
                    });
                }    
                return series;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Se ha producido un error en la búsqueda de la serie", e);
            }

        }

        private class SearchResponse
        {
            [JsonProperty("Search")]
            public List<SerieOmdb> Search { get; set; }
        }

        private class SerieOmdb
        {
            [JsonProperty("Title")]
            public string Titulo { get; set; }

            [JsonProperty("Released")]
            public DateTime FechaLanzamiento { get; set; }

            [JsonProperty("Director")]
            public string Director { get; set; }

            [JsonProperty("Writer")]
            public string Escritor { get; set; }

            [JsonProperty("Actors")]
            public string Actores { get; set; }

            [JsonProperty("Poster")]
            public string FotoPortada { get; set; }

            [JsonProperty("Country")]
            public string PaisOrigen { get; set; }

            [JsonProperty("imdbRating")]
            public string CalificacionIMBD { get; set; }

            [JsonProperty("Runtime")]
            public string Duracion { get; set; }

            [JsonProperty("Genre")]
            public string Genero { get; set; }

            [JsonProperty("Language")]
            public string Idioma { get; set; }
        }
    }

}

