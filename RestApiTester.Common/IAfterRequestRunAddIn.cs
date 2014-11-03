namespace RestApiTester.Common
{
    public interface IAfterRequestRunAddIn
    {
        void Execute(string configurationJson, IRestResponse response, Environment environment, IRestClient restClient);
    }
}
