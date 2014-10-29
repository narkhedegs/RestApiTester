using System;
using NSpec;
using RestApiTester.Common;
using RestApiTester.Tests.Helpers;

namespace RestApiTester.Tests
{
    public class rest_request_populator_specifications : nspec
    {
        private RestRequestPopulator _restRequestPopulator;
        private IRestRequest _unpopulatedRestRequest;
        private IRestRequest _populatedRestRequest;
        private RestRequestCollectionRunConfiguration _collectionRunConfiguration;

        public void when_populating_rest_request()
        {
            before = () =>
            {
                _restRequestPopulator = new RestRequestPopulator();
                _unpopulatedRestRequest = RestRequestGenerator.Default();
                _collectionRunConfiguration = CollectionRunConfigurationGenerator.Default();
            };

            act =
                () =>
                    _populatedRestRequest =
                        _restRequestPopulator.Populate(_unpopulatedRestRequest, _collectionRunConfiguration);

            context["if request parameter is null"] = () =>
            {
                before = () => _unpopulatedRestRequest = null;

                it["should throw ArgumentNullException"] = expect<ArgumentNullException>();
            };

            context["if configuration parameter is null"] = () =>
            {
                before = () => _collectionRunConfiguration = null;;

                it["should return unpopulated request"] = () => _populatedRestRequest.should_be_same(_unpopulatedRestRequest);
            };

            context["if Interpolater is null in configuration"] = () =>
            {
                before = () => _collectionRunConfiguration = CollectionRunConfigurationGenerator.Default().WithNoInterpolater();;

                it["should return unpopulated request"] = () => _populatedRestRequest.should_be_same(_unpopulatedRestRequest);
            };

            context["if Interpolater is not valid i.e. it doesn't contain a word 'variable'"] = () =>
            {
                before = () => _collectionRunConfiguration = CollectionRunConfigurationGenerator.Default().WithInvalidInterpolater(); ;

                it["should throw ArgumentException"] = expect<ArgumentException>();
            };

            context["if Environment is null in configuration"] = () =>
            {
                before = () => _collectionRunConfiguration = CollectionRunConfigurationGenerator.Default().WithNoEnvironment();;

                it["should return unpopulated request"] = () => _populatedRestRequest.should_be_same(_unpopulatedRestRequest);
            };

            context["if Environment Variables are not present in configuration"] = () =>
            {
                before = () => _collectionRunConfiguration = CollectionRunConfigurationGenerator.Default().WithNoEnvironmentVariables();;

                it["should return unpopulated request"] = () => _populatedRestRequest.should_be_same(_unpopulatedRestRequest);
            };

            context["if Request Headers contain place holders"] = () =>
            {
                const string authorizationHeaderValue = "Basic bmFya2hlhdsdjkhfjkhKJHjkhJHjkHdvcmQh";

                before = () =>
                {
                    _unpopulatedRestRequest = RestRequestGenerator.Default()
                        .WithHeader("Authorization", "${Authorization}");
                    _collectionRunConfiguration =
                        CollectionRunConfigurationGenerator.Default()
                            .WithEnvironmentVariable("Authorization", authorizationHeaderValue);
                };

                it["should replace placeholders with actual values from Environment Variables"] =
                    () => _populatedRestRequest.Headers["Authorization"].should_be(authorizationHeaderValue);
            };

            context["if Request Url contain place holders"] = () =>
            {
                const string uri = "www.sample.com";
                const string expectedUrlPath = uri + "/AuthenticationService/CheckPassword";

                before = () =>
                {
                    _unpopulatedRestRequest =
                        RestRequestGenerator.Default().WithUrlPath("${URI}/AuthenticationService/CheckPassword");
                    _collectionRunConfiguration =
                        CollectionRunConfigurationGenerator.Default()
                            .WithEnvironmentVariable("URI", uri);
                };

                it["should replace placeholders with actual values from Environment Variables"] =
                    () => _populatedRestRequest.Url.Path.should_be(expectedUrlPath);
            };
        }
    }
}
