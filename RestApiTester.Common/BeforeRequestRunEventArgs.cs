using System;

namespace RestApiTester.Common
{
    public class BeforeRequestRunEventArgs : EventArgs
    {
        public IRestRequest Request { get; set; }
        public Environment Environment { get; set; }
        public IRestClient RestClient { get; set; }
    }
}