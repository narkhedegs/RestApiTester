using System;

namespace RestApiTester.Common
{
    public interface IRestRequestCollectionItem
    {
        string Id { get; set; }
        string ParentId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        RestRequestCollectionItemType Type { get; set; }
        DateTime? UpdatedDateTime { get; set; }
        IRestRequest Request { get; set; }
    }
}
