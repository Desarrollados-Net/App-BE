using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;//con ctrl + .
using Volo.Abp.Application.Services; //con ctrl + .
//using Volo.Abp.Application.Dtos;//agregue 
//using Volo.Abp.Application.Services;//agregue
using Volo.Abp.Domain.Repositories;

namespace SeriesPlus.Series
{
    public class SerieAppService : CrudAppService <Serie,SerieDto, int, PagedAndSortedResultRequestDto, CreateUpdateSerieDto, CreateUpdateSerieDto>, ISerieAppService
    {
        public SerieAppService(IRepository<Serie, int> repository) :base(repository)//defino el constructor
        {
        }
    }
}
