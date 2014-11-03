namespace RestApiTester.Common
{
    public interface IBeforeRequestRunAddIn
    {
        void Execute(string configurationJson, IRestRequest request, Environment environment, IRestClient restClient);
    }
}
