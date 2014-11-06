using System.Collections.Generic;
using RestApiTester.Common;

namespace RestApiTester
{
    public class RestRequestCollectionRunResult : IRestRequestCollectionRunResult
    {
        public RestRequestCollectionRunResult()
        {
            Responses = new List<IRestResponse>();
        }

        public IRestRequestCollection Collection { get; set; }
        public long ExecutionTime { get; set; }
        public IList<IRestResponse> Responses { get; set; }
    }
}
