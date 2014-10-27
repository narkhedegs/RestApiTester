namespace RestApiTester.Common
{
    public interface IRestRequestCollectionParser
    {
        IRestRequestCollection Parse(string collectionJson);
    }
}