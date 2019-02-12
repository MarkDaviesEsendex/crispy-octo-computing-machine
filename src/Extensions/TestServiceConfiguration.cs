using Microsoft.Azure.WebJobs.Host.Config;

namespace Extensions
{
    public class TestServiceConfiguration : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            context.AddBindingRule<TestServiceAttribute>()
                .BindToInput<ITestService>(_ => new TestService());
        }
    }
}