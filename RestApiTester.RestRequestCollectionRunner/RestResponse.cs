using RestApiTester.Common;

namespace RestApiTester
{
    public interface IRestResponse
    {
        IRestRequest Request { get; set; }
    }

    public class RestResponse : IRestResponse
    {
        public IRestRequest Request { get; set; }
    }
}
