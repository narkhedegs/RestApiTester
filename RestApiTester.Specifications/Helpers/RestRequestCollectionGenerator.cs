using System;
using System.Collections.Generic;
using RestApiTester.Common;

namespace RestApiTester.Specifications.Helpers
{
    public static class RestRequestCollectionGenerator
    {
        public static IRestRequestCollection Default()
        {
            return new FakeRestRequestCollection
            {
                Description = "Sample Collection Description " + DateTime.Now.Ticks,
                Name = "Sample Collection Name " + DateTime.Now.Ticks,
                Id = Guid.NewGuid().ToString(),
                Items = new List<IRestRequestCollectionItem>
                {
                    RestRequestCollectionItemGenerator.Default(),
                    RestRequestCollectionItemGenerator.Default(),
                    RestRequestCollectionItemGenerator.Default()
                }
            };
        }

        public static IRestRequestCollection WithItems(this IRestRequestCollection collection,
            IEnumerable<IRestRequestCollectionItem> items)
        {
            collection.Items = items;
            return collection;
        }
    }

    public class FakeRestRequestCollection : IRestRequestCollection
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<IRestRequestCollectionItem> Items { get; set; }
    }
}
