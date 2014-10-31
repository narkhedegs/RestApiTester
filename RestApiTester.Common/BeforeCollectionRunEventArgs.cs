using System;

namespace RestApiTester.Common
{
    public class BeforeCollectionRunEventArgs : EventArgs
    {
        public IRestRequestCollection Collection { get; set; }
    }
}