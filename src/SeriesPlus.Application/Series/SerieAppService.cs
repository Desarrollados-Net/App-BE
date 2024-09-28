using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SeriesPlus.Series
{
    public class SerieAppService : CrudAppService <Serie,SerieDto, int, PagedAndSortedResultRequestDto, CreateUpdateSerieDto, CreateUpdateSerieDto>, ISerieAppService
    {
        private readonly ISeriesApiService _seriesService;
        public SerieAppService(IRepository<Serie, int> repository, ISeriesApiService seriesService) : base(repository)
        {
            _seriesService = seriesService;
        }
        public async Task<ICollection<SerieDto>> SearchAsync(string titulo, string genero)
        {
            return await _seriesService.GetSeriesAsync(titulo, genero);
        }
    }
}
