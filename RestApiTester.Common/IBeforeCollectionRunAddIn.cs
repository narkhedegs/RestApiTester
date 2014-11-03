namespace RestApiTester.Common
{
    public interface IBeforeCollectionRunAddIn
    {
        void Execute(IRestRequestCollection collection, Environment environment, IRestClient restClient);
    }
}
