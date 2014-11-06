namespace RestApiTester.Common
{
    public interface IAfterCollectionRunAddIn
    {
        void Execute(string configuration, IRestRequestCollectionRunResult collectionRunResult, Environment environment,
            IRestClient restClient);
    }
}
