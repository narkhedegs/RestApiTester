using System;
using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequestCollectionRunner
    {
        event EventHandler<BeforeCollectionRunEventArgs> BeforeCollectionRun;
        event EventHandler<BeforeRequestRunEventArgs> BeforeRequestRun;
        event EventHandler<AfterRequestRunEventArgs> AfterRequestRun;
        event EventHandler<AfterCollectionRunEventArgs> AfterCollectionRun;

        IEnumerable<IRestResponse> Run(
            IRestRequestCollection collection,
            RestRequestCollectionRunConfiguration configuration);
    }
}