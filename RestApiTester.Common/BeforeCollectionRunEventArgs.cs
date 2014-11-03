using System;

namespace RestApiTester.Common
{
    public class BeforeCollectionRunEventArgs : EventArgs
    {
        public IRestRequestCollection Collection { get; set; }
        public Environment Environment { get; set; }
        public IRestClient RestClient { get; set; }
    }
}