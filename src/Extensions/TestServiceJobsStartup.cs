using Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(TestServiceWebJobsStartup))]
namespace Extensions
{
    public class TestServiceWebJobsStartup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddExtension<TestServiceConfiguration>();
        }
    }
}