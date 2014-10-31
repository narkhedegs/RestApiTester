using System;

namespace RestApiTester.Common
{
    public class AfterRequestRunEventArgs : EventArgs
    {
        public IRestResponse Response { get; set; }
    }
}