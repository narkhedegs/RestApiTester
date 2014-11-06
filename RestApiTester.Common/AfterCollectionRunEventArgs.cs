using System;

namespace RestApiTester.Common
{
    public class AfterCollectionRunEventArgs : EventArgs
    {
        public IRestRequestCollectionRunResult CollectionRunResult { get; set; }
        public Environment Environment { get; set; }
        public IRestClient RestClient { get; set; }
    }
}