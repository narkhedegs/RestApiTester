namespace RestApiTester.Common
{
    public interface IAfterRequestRunAddIn
    {
        void Execute(IRestResponse response, Environment environment, IRestClient restClient);
    }
}
