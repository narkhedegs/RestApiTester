using RestApiTester.Common;

namespace RestApiTester.Specifications.Helpers
{
    public static class RestResponseGenerator
    {
        public static IRestResponse Default()
        {
            return new RestResponse();
        }

        public static IRestResponse WithExecutionTime(this IRestResponse response, long executionTime)
        {
            response.ExecutionTime = executionTime;
            return response;
        }
    }
}
