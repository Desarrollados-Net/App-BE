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

    public class OmdbService : ISeriesApiService, ITransientDependency
    {
        private static readonly string apiKey = "39e61792"; // Reemplaza con tu clave API de OMDb.
        private static readonly string baseUrl = "http://www.omdbapi.com/";


        public async Task<SerieDto[]> BuscarSerieAsync(string titulo, string genero = null)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("El campo título es obligatorio para la búsqueda.", nameof(titulo));
            }

            if (!string.IsNullOrWhiteSpace(titulo) && string.IsNullOrWhiteSpace(genero))
            {
                return await BuscarPorTituloAsync(titulo);
            }

            if (!string.IsNullOrWhiteSpace(titulo) && !string.IsNullOrWhiteSpace(genero))
            {
                return await BuscarPorTituloYGeneroAsync(titulo, genero);
            }

            throw new ArgumentException("No se puede buscar solo por género. El título es obligatorio.");
        }

        private async Task<SerieDto[]> BuscarPorTituloAsync(string titulo)
        {
            var url = $"{baseUrl}?apiKey={apiKey}&s={titulo}&type=series";
            return await ObtenerSeriesDesdeOmdbAsync(url);
        }

        private async Task<SerieDto[]> BuscarPorTituloYGeneroAsync(string titulo, string genero)
        {
            var url = $"{baseUrl}?apiKey={apiKey}&s={titulo}&type=series";
            var series = await ObtenerSeriesDesdeOmdbAsync(url);

            var seriesFiltradas = new List<SerieDto>();
            foreach (var serie in series)
            {
                if (serie.Genero != null && serie.Genero.Contains(genero, StringComparison.OrdinalIgnoreCase))
                {
                    seriesFiltradas.Add(serie);
                }
            }

            return seriesFiltradas.ToArray();
        }

        private async Task<SerieDto[]> ObtenerSeriesDesdeOmdbAsync(string url)
        {
            // Inicializar el monitoreo del tiempo
            var monitoreo = new MonitoreoApi
            {
                HoraEntrada = DateTime.Now // Registrar la hora de entrada
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                monitoreo.HoraSalida = DateTime.Now; // Registrar la hora de salida

                // Aquí podemos agregar el código para almacenar o procesar el monitoreo, si es necesario.

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonResponse);

                if (json["Response"]?.ToString() == "False")
                {
                    return Array.Empty<SerieDto>();
                }

                var seriesJson = json["Search"];
                if (seriesJson == null)
                {
                    return Array.Empty<SerieDto>();
                }

                var seriesList = new List<SerieDto>();
                foreach (var serie in seriesJson)
                {
                    var serieId = serie["imdbID"]?.ToString();
                    var serieDetails = await ObtenerDetallesSerieAsync(serieId);

                    if (serieDetails != null)
                    {
                        seriesList.Add(serieDetails);
                    }
                }

                return seriesList.ToArray();
            }
        }

        public async Task<TemporadaDto> BuscarTemporadaAsync(string imdbId, int numeroTemporada)
        {
            if (string.IsNullOrWhiteSpace(imdbId))
            {
                throw new ArgumentException("El identificador IMDb es obligatorio para buscar una temporada.", nameof(imdbId));
            }

            var url = $"{baseUrl}?apiKey={apiKey}&i={imdbId}&season={numeroTemporada}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonResponse);

                if (json["Response"]?.ToString() == "False")
                {
                    return null;
                }

                var episodiosJson = json["Episodes"];
                if (episodiosJson == null)
                {
                    return null;
                }

                var episodiosList = new List<EpisodioDto>();
                foreach (var episodio in episodiosJson)
                {
                    episodiosList.Add(new EpisodioDto
                    {
                        Titulo = episodio["Title"]?.ToString(),
                        NumeroEpisodio = int.TryParse(episodio["Episode"]?.ToString(), out var episodioNum) ? episodioNum : 0,
                        FechaEstreno = DateOnly.TryParse(episodio["Released"]?.ToString(), out var fecha) ? fecha : DateOnly.MinValue
                    });
                }

                return new TemporadaDto
                {
                    Titulo = json["Title"]?.ToString(),
                    NumeroTemporada = int.TryParse(json["Season"]?.ToString(), out var seasonNumber) ? seasonNumber : 0,
                    Episodios = episodiosList
                };
            }
        }


        private async Task<SerieDto> ObtenerDetallesSerieAsync(string imdbId)
        {
            var url = $"{baseUrl}?apiKey={apiKey}&i={imdbId}";

            // Inicializar el monitoreo del tiempo
            var monitoreo = new MonitoreoApi
            {
                HoraEntrada = DateTime.Now // Registrar la hora de entrada
            };

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                monitoreo.HoraSalida = DateTime.Now; // Registrar la hora de salida

                // Aquí podemos agregar el código para almacenar o procesar el monitoreo, si es necesario.

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonResponse);

                return new SerieDto
                {
                    Titulo = json["Title"]?.ToString(),
                    FechaLanzamiento = json["Released"]?.ToString(),
                    Duracion = json["Runtime"]?.ToString(),
                    Genero = json["Genre"]?.ToString(),
                    Director = json["Director"]?.ToString(),
                    Escritor = json["Writer"]?.ToString(),
                    Actores = json["Actors"]?.ToString(),
                    Idioma = json["Language"]?.ToString(),
                    PaisOrigen = json["Country"]?.ToString(),
                    FotoPortada = json["Poster"]?.ToString(),
                    CalificacionIMDB = json["imdbRating"]?.ToString(),
                    TotalTemporadas = int.TryParse(json["totalSeasons"]?.ToString(), out var seasons) ? seasons : 0
                };
            }
        }

        public async Task<ICollection<SerieDto>> GetSeriesAsync(string Titulo) //string genero
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

                // Imprimir la respuesta JSON para depurar
                Console.WriteLine(jsonResponse); // Agrega esta línea (para ver/verificar datos q  de la api por consola)

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
                        CalificacionIMDB = serieOmdb.CalificacionIMDB,
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
            public string FechaLanzamiento { get; set; }

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
            public string CalificacionIMDB { get; set; }

            [JsonProperty("Runtime")]
            public string Duracion { get; set; }

            [JsonProperty("Genre")]
            public string Genero { get; set; }

            [JsonProperty("Language")]
            public string Idioma { get; set; }
        }
    }

}