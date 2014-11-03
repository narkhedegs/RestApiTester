namespace RestApiTester.Common
{
    public interface IBeforeCollectionRunAddIn
    {
        void Execute(string configurationJson, IRestRequestCollection collection, Environment environment, IRestClient restClient);
    }
}
