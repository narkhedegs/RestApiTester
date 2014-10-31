using System;
using System.Collections.Generic;
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

            if (BeforeCollectionRun != null)
            {
                BeforeCollectionRun(this, new BeforeCollectionRunEventArgs {Collection = collection});
            }

            foreach (var item in collection.Items)
            {
                if (item.Type == RestRequestCollectionItemType.Request)
                {
                    var populatedRequest = _requestPopulator.Populate(item.Request, configuration);

                    if (BeforeRequestRun != null)
                    {
                        BeforeRequestRun(this, new BeforeRequestRunEventArgs {Request = populatedRequest});
                    }

                    var restResponse = _restClient.Execute(populatedRequest);

                    if (AfterRequestRun != null)
                    {
                        AfterRequestRun(this, new AfterRequestRunEventArgs {Response = restResponse});
                    }

                    restResponses.Add(restResponse);
                }
            }

            if (AfterCollectionRun != null) AfterCollectionRun(this, new AfterCollectionRunEventArgs());

            return restResponses;
        }
    }
}
