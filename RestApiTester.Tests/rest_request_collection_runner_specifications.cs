using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NSpec;
using RestApiTester.Common;
using RestApiTester.Tests.Helpers;

namespace RestApiTester.Tests
{
    public class rest_request_collection_runner_specifications : nspec
    {
        private RestRequestCollectionRunner _collectionRunner;
        private Mock<IRestRequestPopulator> _requestPopulator;
        private IEnumerable<IRestResponse> _collectionRunResult;
        private IRestRequestCollection _collection;
        private RestRequestCollectionRunConfiguration _configuration;
        private Mock<IRestClient> _restClient;

        public void when_running_a_rest_request_collection()
        {
            List<string> raisedEvents = null;

            before = () =>
            {
                raisedEvents = new List<string>();

                _requestPopulator = new Mock<IRestRequestPopulator>();
                _restClient = new Mock<IRestClient>();

                _collectionRunner = new RestRequestCollectionRunner(
                    _requestPopulator.Object,
                    _restClient.Object);

                _collection = RestRequestCollectionGenerator.Default();
                _configuration = CollectionRunConfigurationGenerator.Default();
            };

            act = () => _collectionRunResult = _collectionRunner.Run(_collection, _configuration);

            context["BeforeCollectionRun Event"] = () =>
            {
                BeforeCollectionRunEventArgs beforeCollectionRunEventArgs = null;

                before = () =>
                {
                    _collectionRunner.BeforeCollectionRun += delegate(object sender, BeforeCollectionRunEventArgs eventArgs)
                    {
                        beforeCollectionRunEventArgs = eventArgs;
                        raisedEvents.Add("BeforeCollectionRun"); 
                    };
                };

                it["should raise BeforeCollectionRun event"] = () => raisedEvents.should_contain(eventName => eventName == "BeforeCollectionRun");
                it["BeforeCollectionRunEventArgs should not be null"] =
                    () => beforeCollectionRunEventArgs.should_not_be_null();
                it["BeforeCollectionRunEventArgs Collection should contain RestRequestCollection"] =
                    () => beforeCollectionRunEventArgs.Collection.should_be(_collection);
            };

            context["BeforeRequestRun Event"] = () =>
            {
                BeforeRequestRunEventArgs beforeRequestRunEventArgs = null;

                before = () =>
                {
                    _collectionRunner.BeforeRequestRun += delegate(object sender, BeforeRequestRunEventArgs eventArgs)
                    {
                        beforeRequestRunEventArgs = eventArgs;
                        raisedEvents.Add("BeforeRequestRun");
                    };
                };

                it["should raise BeforeCollectionRun event"] = () => raisedEvents.should_contain(eventName => eventName == "BeforeRequestRun");
                it["BeforeCollectionRunEventArgs should not be null"] =
                    () => beforeRequestRunEventArgs.should_not_be_null();
            };

            context["AfterRequestRun Event"] = () =>
            {
                AfterRequestRunEventArgs afterRequestRunEventArgs = null;

                before = () =>
                {
                    _collectionRunner.AfterRequestRun += delegate(object sender, AfterRequestRunEventArgs eventArgs)
                    {
                        afterRequestRunEventArgs = eventArgs;
                        raisedEvents.Add("AfterRequestRun");
                    };
                };

                it["should raise AfterRequestRun event"] = () => raisedEvents.should_contain(eventName => eventName == "AfterRequestRun");
                it["AfterRequestRunEventArgs should not be null"] =
                    () => afterRequestRunEventArgs.should_not_be_null();
            };

            context["AfterCollectionRun Event"] = () =>
            {
                AfterCollectionRunEventArgs afterCollectionRunEventArgs = null;

                before = () =>
                {
                    _collectionRunner.AfterCollectionRun += delegate(object sender, AfterCollectionRunEventArgs eventArgs)
                    {
                        afterCollectionRunEventArgs = eventArgs;
                        raisedEvents.Add("AfterCollectionRun");
                    };
                };

                it["should raise AfterCollectionRun event"] = () => raisedEvents.should_contain(eventName => eventName == "AfterCollectionRun");
                it["AfterCollectionRunEventArgs should not be null"] =
                    () => afterCollectionRunEventArgs.should_not_be_null();
            };

            context["if Collection contains 3 Request Items and 1 Project Item"] = () =>
            {
                before = () =>
                {
                    _collection =
                        RestRequestCollectionGenerator.Default()
                            .WithItems(new List<IRestRequestCollectionItem>
                            {
                                RestRequestCollectionItemGenerator.Default().WithType(RestRequestCollectionItemType.Request),
                                RestRequestCollectionItemGenerator.Default().WithType(RestRequestCollectionItemType.Request),
                                RestRequestCollectionItemGenerator.Default().WithType(RestRequestCollectionItemType.Request),
                                RestRequestCollectionItemGenerator.Default().WithType(RestRequestCollectionItemType.Project)
                            });
                };

                it["should call Request Populator 3 times"] =
                    () =>
                        _requestPopulator.Verify(
                            populator =>
                                populator.Populate(It.IsAny<IRestRequest>(),
                                    It.IsAny<RestRequestCollectionRunConfiguration>()), Times.Exactly(3));                
                it["should call RestClient 3 times"] =
                    () =>
                        _restClient.Verify(
                            client => client.Execute(It.IsAny<IRestRequest>()),
                            Times.Exactly(3));
                it["the collection run result should contain 3 Rest Responses"] =
                    () => _collectionRunResult.Count().should_be(3);
            };

            context["if the collection is null"] = () =>
            {
                before = () => _collection = null;

                it["should throw ArgumentNullException"] = expect<ArgumentNullException>();
            };
        }
    }
}
