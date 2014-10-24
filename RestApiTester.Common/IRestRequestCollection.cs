using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequestCollection
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        List<IRestRequestCollectionItem> Items { get; set; }
    }
}
