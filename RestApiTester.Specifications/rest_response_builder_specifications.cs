using System;
using System.Net;
using System.Text;
using NSpec;
using NSpec.Domain.Extensions;
using RestApiTester.Specifications.Helpers;
using RestSharp;
using IRestRequest = RestApiTester.Common.IRestRequest;
using IRestResponse = RestApiTester.Common.IRestResponse;
using ResponseStatus = RestApiTester.Common.ResponseStatus;

namespace RestApiTester.Specifications
{
    public class rest_response_builder_specifications : nspec
    {
        private IRestResponseBuilder _restResponseBuilder;
        private IRestResponse _restResponse;
        private RestSharp.IRestResponse _restSharpRestResponse;
        private IRestRequest _restRequest;

        public void when_building_rest_response()
        {
            before = () =>
            {
                _restResponseBuilder = new RestResponseBuilder();

                _restRequest = RestRequestGenerator.Default();
                _restSharpRestResponse = new RestSharp.RestResponse
                {
                    ContentType = "application/json",
                    ContentLength = 100,
                    ContentEncoding = Encoding.ASCII.ToString(),
                    Content = "Sample Content",
                    StatusCode = HttpStatusCode.Found,
                    StatusDescription = "Found",
                    RawBytes = new byte[]{1,0,1},
                    Server = "api.gsn.com",
                    ResponseStatus = RestSharp.ResponseStatus.Completed,
                    ErrorMessage = "Sample Error Message",
                    ErrorException = new Exception("Sample Exception")
                };
                _restSharpRestResponse.Headers.Add(new Parameter
                {
                    Name = "Authorization",
                    Value = "Basic khjsdjfhjhGjGHgHgJHgHJGHgHgHGHGJG=="
                });                
                _restSharpRestResponse.Headers.Add(new Parameter
                {
                    Name = "Token",
                    Value = "KJHJhHHjHJhKhJhjkhjhjhKJjhghG8hJH"
                });
            };

            act = () => _restResponse = _restResponseBuilder.Build(_restSharpRestResponse, _restRequest);

            it["should populate RestRequest"] = () => _restResponse.Request.should_be(_restRequest);
            it["should populate ContentType"] =
                () => _restResponse.ContentType.should_be(_restSharpRestResponse.ContentType);
            it["should populate ContentLength"] =
                () => _restResponse.ContentLength.should_be(_restSharpRestResponse.ContentLength);
            it["should populate ContentEncoding"] =
                () => _restResponse.ContentEncoding.should_be(_restSharpRestResponse.ContentEncoding);
            it["should populate Content"] =
                () => _restResponse.Content.should_be(_restSharpRestResponse.Content);
            it["should populate StatusCode"] =
                () => _restResponse.StatusCode.should_be(_restSharpRestResponse.StatusCode);
            it["should populate StatusDescription"] =
                () => _restResponse.StatusDescription.should_be(_restSharpRestResponse.StatusDescription);
            it["should populate RawBytes"] =
                () => _restResponse.RawBytes.should_be(_restSharpRestResponse.RawBytes);
            it["should populate Server"] =
                () => _restResponse.Server.should_be(_restSharpRestResponse.Server);
            it["should populate Headers"] =
                () =>
                    _restResponse.Headers.Each(
                        header =>
                            _restSharpRestResponse.Headers.should_contain(
                                restSharpHeader =>
                                    restSharpHeader.Name == header.Key && restSharpHeader.Value.ToString() == header.Value));
            it["should populate ResponseStatus"] =
                () =>
                    _restResponse.ResponseStatus.should_be(
                        (ResponseStatus)
                            Enum.Parse(typeof (ResponseStatus), _restSharpRestResponse.ResponseStatus.ToString(), true));
            it["should populate ErrorMessage"] =
                () => _restResponse.ErrorMessage.should_be(_restSharpRestResponse.ErrorMessage);
            it["should populate ErrorException"] =
                () => _restResponse.ErrorException.should_be(_restSharpRestResponse.ErrorException);

            context["if restSharpRestResponse parameter is null"] = () =>
            {
                before = () => _restSharpRestResponse = null;

                it["should throw ArgumentNullException"] = expect<ArgumentNullException>();
            };

            context["if restRequest parameter is null"] = () =>
            {
                before = () => _restRequest = null;

                it["should throw ArgumentNullException"] = expect<ArgumentNullException>();
            };
        }
    }
}
