using System;
using System.Collections.Generic;
using System.Linq;
using RestApiTester.Common;

namespace RestApiTester
{
    public class RestRequestCollectionRunner : IRestRequestCollectionRunner
    {
        private readonly IRestRequestPopulator _requestPopulator;
        private readonly IRestClient _restClient;

        public RestRequestCollectionRunner(
            IRestRequestPopulator requestPopulator,
            IRestClient restClient)
        {
            _requestPopulator = requestPopulator;
            _restClient = restClient;
        }

        public event EventHandler<BeforeCollectionRunEventArgs> BeforeCollectionRun;
        public event EventHandler<BeforeRequestRunEventArgs> BeforeRequestRun;
        public event EventHandler<AfterRequestRunEventArgs> AfterRequestRun;
        public event EventHandler<AfterCollectionRunEventArgs> AfterCollectionRun;

        public IEnumerable<IRestResponse> Run(
            IRestRequestCollection collection,
            RestRequestCollectionRunConfiguration configuration)
        {
            if (collection == null) throw new ArgumentNullException("collection");

            var restResponses = new List<IRestResponse>();

            collection.Items.ToList().ForEach(item =>
            {
                if (item.Type == RestRequestCollectionItemType.Request)
                {
                    item.Request = _requestPopulator.Populate(item.Request, configuration);
                }
            });

            if (BeforeCollectionRun != null)
            {
                BeforeCollectionRun(this, new BeforeCollectionRunEventArgs
                {
                    Collection = collection,
                    Environment = configuration.Environment,
                    RestClient = _restClient
                });
            }

            foreach (var item in collection.Items)
            {
                if (item.Type == RestRequestCollectionItemType.Request)
                {
                    if (BeforeRequestRun != null)
                    {
                        BeforeRequestRun(this, new BeforeRequestRunEventArgs
                        {
                            Request = item.Request,
                            Environment = configuration.Environment,
                            RestClient = _restClient
                        });
                    }

                    var restResponse = _restClient.Execute(item.Request);

                    if (AfterRequestRun != null)
                    {
                        AfterRequestRun(this, new AfterRequestRunEventArgs
                        {
                            Response = restResponse,
                            Environment = configuration.Environment,
                            RestClient = _restClient
                        });
                    }

                    restResponses.Add(restResponse);
                }
            }

            if (AfterCollectionRun != null)
            {
                AfterCollectionRun(this, new AfterCollectionRunEventArgs());
            }

            return restResponses;
        }
    }
}
