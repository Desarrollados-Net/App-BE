using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SeriesPlus.Series
{
    public class OmdbService : ISeriesApiService
    {
        private static readonly string apiKey = "39e61792";
        private static readonly string baseUrl = "http://www.omdbapi.com/";

        public async Task<ICollection<SerieDto>> GetSeriesAsync(string Titulo, string Genero)
        {
            using HttpClient client = new HttpClient();
            List<SerieDto> series = new List<SerieDto>();

            string url = $"{baseUrl}?s={Titulo}&apikey={apiKey}&type=series";

            try
            {
                // Hacer la solicitud HTTP y obtener la respuesta como string
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserializar la respuesta JSON a un objeto SearchResponse
                var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(jsonResponse);

                // Retornar la lista de series si existen
                var seriesOmdb = searchResponse?.Search ?? new List<SerieOmdb>();

                foreach (var serieOmdb in seriesOmdb)
                {
                    series.Add(new SerieDto { Titulo = serieOmdb.Title });
                }

                return series;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Se ha producido un error en la búsqueda de la serie", e);
            }
        }

        // Agregado para solucionar errores
        Task<SerieDto[]> ISeriesApiService.GetSeriesAsync(string Titulo, string Genero)
        {
            throw new NotImplementedException();
        }

        private class SearchResponse
        {
            [JsonProperty("Search")]
            public List<SerieOmdb> Search { get; set; }
        }
        private class SerieOmdb
        {
            public string Title { get; set; }
            public string ReleaseDate { get; set; }
            public string Director { get; set; }
            public string Actors { get; set; }
        }
    }
}
