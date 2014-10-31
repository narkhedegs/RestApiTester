using System;

namespace RestApiTester.Common
{
    public class BeforeRequestRunEventArgs : EventArgs
    {
        public IRestRequest Request { get; set; }
    }
}