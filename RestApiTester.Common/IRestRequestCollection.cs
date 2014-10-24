using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequestCollection<T> where T : IRestRequestCollectionItem
    {
        string Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        IEnumerable<T> Items { get; set; }
    }
}
