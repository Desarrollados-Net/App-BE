using Xunit;

namespace SeriesPlus.EntityFrameworkCore;

[CollectionDefinition(SeriesPlusTestConsts.CollectionDefinitionName)]
public class SeriesPlusEntityFrameworkCoreCollection : ICollectionFixture<SeriesPlusEntityFrameworkCoreFixture>
{

}
