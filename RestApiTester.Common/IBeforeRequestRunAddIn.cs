namespace RestApiTester.Common
{
    public interface IBeforeRequestRunAddIn
    {
        void Execute(IRestRequest request, Environment environment, IRestClient restClient);
    }
}
