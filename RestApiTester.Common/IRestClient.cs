namespace RestApiTester.Common
{
    public interface IRestClient
    {
        IRestResponse Execute(IRestRequest request);
    }
}