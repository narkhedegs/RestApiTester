using System.Collections.Generic;

namespace RestApiTester.Common
{
    public interface IRestRequestCollectionRunResult
    {
        IRestRequestCollection Collection { get; set; }
        long ExecutionTime { get; set; }
        IList<IRestResponse> Responses { get; set; } 
    }
}
