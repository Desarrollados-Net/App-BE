using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeriesPlus.EntityFrameworkCore;
using Xunit;

//namespace SeriesPlus.EntityFrameworkCore.Domains.Series
namespace SeriesPlus.Series
{
    [Collection(SeriesPlusTestConsts.CollectionDefinitionName)]
    public class EfCoreOmdbService_Tests : OmdbService_Tests<SeriesPlusEntityFrameworkCoreTestModule>
    {
    }
}
