using System;

namespace RestApiTester.Common
{
    public class AfterRequestRunEventArgs : EventArgs
    {
        public IRestResponse Response { get; set; }
        public Environment Environment { get; set; }
        public IRestClient RestClient { get; set; }
    }
}