using System;
using System.Linq;
using RestApiTester.Common;

namespace RestApiTester
{
    public interface IRestRequestPopulator
    {
        IRestRequest Populate(IRestRequest request, RestRequestCollectionRunConfiguration configuration);
    }

    public class RestRequestPopulator : IRestRequestPopulator
    {
        public IRestRequest Populate(IRestRequest request, RestRequestCollectionRunConfiguration configuration)
        {
            if(request == null) throw new ArgumentNullException("request");
            if (configuration == null || 
                string.IsNullOrWhiteSpace(configuration.Interpolater) || 
                configuration.Environment == null ||
                configuration.Environment.Variables == null || 
                configuration.Environment.Variables.Count <= 0)
            {
                return request;
            }

            if (!configuration.Interpolater.ToLower().Contains("variable".ToLower()))
            {
                throw new ArgumentException("Interpolater should contain a word 'variable'.");
            }

            foreach (var environmentVariable in configuration.Environment.Variables)
            {
                var placeHolder = configuration.Interpolater.Replace("variable", environmentVariable.Key);
                var placeHolderValue = environmentVariable.Value;

                foreach (var header in request.Headers.ToList())
                {
                    if (header.Value.Contains(placeHolder))
                    {
                        request.Headers[header.Key] = header.Value.Replace(placeHolder, placeHolderValue);
                    }
                }

                request.Url.Path = request.Url.Path.Replace(placeHolder, placeHolderValue);
            }

            return request;
        }
    }
}
