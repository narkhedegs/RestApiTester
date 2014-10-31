using System;
using RestApiTester.Common;

namespace RestApiTester.Tests.Helpers
{
    public static class RestRequestCollectionItemGenerator
    {
        public static IRestRequestCollectionItem Default()
        {
            return new FakeRestRequestCollectionItem
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Sample Description " + DateTime.Now.Ticks,
                Name = "Sample Name " + DateTime.Now.Ticks,
                ParentId = Guid.NewGuid().ToString(),
                Type = RestRequestCollectionItemType.Request,
                UpdatedDateTime = DateTime.Now,
                Request = RestRequestGenerator.Default()
            };
        }

        public static IRestRequestCollectionItem WithType(this IRestRequestCollectionItem item,RestRequestCollectionItemType type)
        {
            item.Type = type;
            return item;
        }
    }

    public class FakeRestRequestCollectionItem : IRestRequestCollectionItem
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RestRequestCollectionItemType Type { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
        public IRestRequest Request { get; set; }
    }
}
