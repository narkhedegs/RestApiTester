using System.Collections.Generic;
using RestApiTester.Common;

namespace RestApiTester.Tests.Helpers
{
    public static class CollectionRunConfigurationGenerator
    {
        public static RestRequestCollectionRunConfiguration Default()
        {
            return new RestRequestCollectionRunConfiguration
            {
                Interpolater = @"${variable}",
                Environment = new Environment
                {
                    Name = "Development",
                    Variables = new Dictionary<string, string>
                    {
                        {"SampleVariable","SampleVariableValue"}
                    }
                }
            };
        }

        public static RestRequestCollectionRunConfiguration WithNoInterpolater(this RestRequestCollectionRunConfiguration configuration)
        {
            configuration.Interpolater = string.Empty;
            return configuration;
        }    
    
        public static RestRequestCollectionRunConfiguration WithInvalidInterpolater(this RestRequestCollectionRunConfiguration configuration)
        {
            configuration.Interpolater = @"${not valid}";
            return configuration;
        }        
        
        public static RestRequestCollectionRunConfiguration WithNoEnvironment(this RestRequestCollectionRunConfiguration configuration)
        {
            configuration.Environment = null;
            return configuration;
        }

        public static RestRequestCollectionRunConfiguration WithNoEnvironmentVariables(this RestRequestCollectionRunConfiguration configuration)
        {
            configuration.Environment.Variables = new Dictionary<string, string>();
            return configuration;
        }

        public static RestRequestCollectionRunConfiguration WithEnvironmentVariable(this RestRequestCollectionRunConfiguration configuration, string key, string value)
        {
            configuration.Environment.Variables.Add(key,value);
            return configuration;
        }
    }
}
