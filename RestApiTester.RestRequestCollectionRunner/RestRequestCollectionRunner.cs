using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using RestApiTester.Common;

namespace RestApiTester
{
    public class RestRequestCollectionRunner : IRestRequestCollectionRunner
    {
        public event EventHandler<BeforeCollectionRunEventArgs> BeforeCollectionRun;
        public event EventHandler<BeforeRequestRunEventArgs> BeforeRequestRun;
        public event EventHandler<AfterRequestRunEventArgs> AfterRequestRun;
        public event EventHandler<AfterCollectionRunEventArgs> AfterCollectionRun;

        private readonly IRestRequestPopulator _requestPopulator;
        private readonly IRestClient _restClient;

        public RestRequestCollectionRunner(
            IRestRequestPopulator requestPopulator,
            IRestClient restClient)
        {
            _requestPopulator = requestPopulator;
            _restClient = restClient;
        }

        public IRestRequestCollectionRunResult Run(
            IRestRequestCollection collection,
            RestRequestCollectionRunConfiguration configuration)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            var restResponses = new List<IRestResponse>();
            var collectionRunExecutionTimeCounter = new Stopwatch();
            var requestRunExecutionTimeCounter = new Stopwatch();

            collection.Items.ToList().ForEach(item =>
            {
                if (item.Type == RestRequestCollectionItemType.Request)
                {
                    item.Request = _requestPopulator.Populate(item.Request, configuration);
                }
            });

            RaiseBeforeCollectionRunEvent(configuration, collection, _restClient);

            collectionRunExecutionTimeCounter.Start();
            foreach (var item in collection.Items)
            {
                if (item.Type == RestRequestCollectionItemType.Request)
                {
                    var populatedRequest = _requestPopulator.Populate(item.Request, configuration);

                    RaiseBeforeRequestRunEvent(configuration, populatedRequest, _restClient);
                    
                    requestRunExecutionTimeCounter.Restart();
                    var restResponse = _restClient.Execute(populatedRequest);
                    requestRunExecutionTimeCounter.Stop();
                    restResponse.ExecutionTime = requestRunExecutionTimeCounter.ElapsedMilliseconds;

                    RaiseAfterRequestRunEvent(configuration, restResponse, _restClient);

                    restResponses.Add(restResponse);
                }
            }
            collectionRunExecutionTimeCounter.Stop();

            var collectionRunResult = new RestRequestCollectionRunResult
            {
                Collection = collection,
                ExecutionTime = collectionRunExecutionTimeCounter.ElapsedMilliseconds,
                Responses = restResponses
            };

            RaiseAfterCollectionRunEvent(configuration, collectionRunResult, _restClient);

            return collectionRunResult;
        }

        private void RaiseAfterCollectionRunEvent(RestRequestCollectionRunConfiguration configuration,
            IRestRequestCollectionRunResult collectionRunResult, IRestClient restClient)
        {
            if (AfterCollectionRun != null)
            {
                AfterCollectionRun(this, new AfterCollectionRunEventArgs
                {
                    CollectionRunResult = collectionRunResult,
                    Environment = configuration.Environment,
                    RestClient = restClient
                });
            }
        }

        private void RaiseAfterRequestRunEvent(RestRequestCollectionRunConfiguration configuration,
            IRestResponse restResponse, IRestClient restClient)
        {
            if (AfterRequestRun != null)
            {
                AfterRequestRun(this, new AfterRequestRunEventArgs
                {
                    Response = restResponse,
                    Environment = configuration.Environment,
                    RestClient = restClient
                });
            }
        }

        private void RaiseBeforeRequestRunEvent(RestRequestCollectionRunConfiguration configuration,
            IRestRequest populatedRequest, IRestClient restClient)
        {
            if (BeforeRequestRun != null)
            {
                BeforeRequestRun(this, new BeforeRequestRunEventArgs
                {
                    Request = populatedRequest,
                    Environment = configuration.Environment,
                    RestClient = restClient
                });
            }
        }

        private void RaiseBeforeCollectionRunEvent(RestRequestCollectionRunConfiguration configuration,
            IRestRequestCollection collection, IRestClient restClient)
        {
            if (BeforeCollectionRun != null)
            {
                BeforeCollectionRun(this, new BeforeCollectionRunEventArgs
                {
                    Collection = collection,
                    Environment = configuration.Environment,
                    RestClient = restClient
                });
            }
        }
    }
}
