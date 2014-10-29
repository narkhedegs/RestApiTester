using System;
using NSpec;

namespace RestApiTester.Tests
{
    public class rest_request_collection_runner_specifications : nspec
    {
        private RestRequestCollectionRunner _collectionRunner;

        public void when_running_a_rest_request_collection()
        {
            before = () =>
            {
                _collectionRunner = new RestRequestCollectionRunner();
            };

            context["if the collection is null"] = () =>
            {
                it["should throw ArgumentNullException"] = expect<ArgumentNullException>(() => _collectionRunner.Run(null));
            };
        }
    }
}
