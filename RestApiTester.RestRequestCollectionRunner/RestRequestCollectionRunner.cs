using System;
using System.Collections.Generic;
using RestApiTester.Common;
using RestSharp;

namespace RestApiTester
{
    public interface IRestRequestCollectionRunner
    {
        IEnumerable<IRestResponse> Run(IRestRequestCollection collection);
    }

    public class RestRequestCollectionRunner : IRestRequestCollectionRunner
    {
        public IEnumerable<IRestResponse> Run(IRestRequestCollection collection)
        {
            if(collection == null) throw new ArgumentNullException("collection");

            var restResponses = new List<IRestResponse>();

            foreach (var item in collection.Items)
            {
                var restClient = new RestClient();
            }

            return restResponses;
        }
    }
}
