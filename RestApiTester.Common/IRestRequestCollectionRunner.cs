using System;

namespace RestApiTester.Common
{
    public interface IRestRequestCollectionRunner
    {
        event EventHandler<BeforeCollectionRunEventArgs> BeforeCollectionRun;
        event EventHandler<BeforeRequestRunEventArgs> BeforeRequestRun;
        event EventHandler<AfterRequestRunEventArgs> AfterRequestRun;
        event EventHandler<AfterCollectionRunEventArgs> AfterCollectionRun;

        IRestRequestCollectionRunResult Run(
            IRestRequestCollection collection,
            RestRequestCollectionRunConfiguration configuration);
    }
}