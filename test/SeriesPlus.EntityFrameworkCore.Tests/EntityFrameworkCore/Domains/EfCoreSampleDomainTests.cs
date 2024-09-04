using SeriesPlus.Samples;
using Xunit;

namespace SeriesPlus.EntityFrameworkCore.Domains;

[Collection(SeriesPlusTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<SeriesPlusEntityFrameworkCoreTestModule>
{

}
