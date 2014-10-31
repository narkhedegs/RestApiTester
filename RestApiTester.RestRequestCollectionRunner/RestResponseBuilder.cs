using System;
using RestApiTester.Common;

namespace RestApiTester
{
    public interface IRestResponseBuilder
    {
        IRestResponse Build(
            RestSharp.IRestResponse restSharpRestResponse,
            IRestRequest restRequest);
    }

    public class RestResponseBuilder : IRestResponseBuilder
    {
        public IRestResponse Build(RestSharp.IRestResponse restSharpRestResponse, IRestRequest restRequest)
        {
            if(restSharpRestResponse == null) throw new ArgumentNullException("restSharpRestResponse");
            if(restRequest == null) throw new ArgumentNullException("restRequest");

            var restResponse = new RestResponse();
            restResponse.Request = restRequest;
            restResponse.ContentType = restSharpRestResponse.ContentType;
            restResponse.ContentLength = restSharpRestResponse.ContentLength;
            restResponse.ContentEncoding = restSharpRestResponse.ContentEncoding;
            restResponse.Content = restSharpRestResponse.Content;
            restResponse.StatusCode = restSharpRestResponse.StatusCode;
            restResponse.StatusDescription = restSharpRestResponse.StatusDescription;
            restResponse.RawBytes = restSharpRestResponse.RawBytes;
            restResponse.Server = restSharpRestResponse.Server;

            foreach (var parameter in restSharpRestResponse.Headers)
            {
                restResponse.Headers.Add(parameter.Name, Convert.ToString(parameter.Value));
            }

            restResponse.ResponseStatus = (ResponseStatus)
                            Enum.Parse(typeof(ResponseStatus), restSharpRestResponse.ResponseStatus.ToString(), true);
            restResponse.ErrorMessage = restSharpRestResponse.ErrorMessage;
            restResponse.ErrorException = restSharpRestResponse.ErrorException;

            return restResponse;
        }
    }
}
