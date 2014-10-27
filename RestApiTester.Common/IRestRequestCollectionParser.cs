namespace RestApiTester.Common
{
    public interface IRestRequestCollectionParser
    {
        IRestRequestCollection<IRestRequestCollectionItem> Parse(string collectionJson);
    }
}