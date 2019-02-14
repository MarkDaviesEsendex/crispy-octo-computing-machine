# crispy-octo-computing-machine

## What is this?
This repo is just an example of how you can register your own input bindings in azure functions

## Where should I be looking?
In the `Extensions` project I have a simple class called `TestServiceWebJobsStartup` that looks like this:
```
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
```
This tells the azure function host to call this Configuer function when the function is spinning up, much like the Configure in any other web framework. Here I register a `TestServiceConfiguration` which looks something like this:

```
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
```
This tells the function that when it sees the `TestServiceAttribute` then it can bind the class `TestService`. The attribute in this project is pretty plain:
```
using System;
using Microsoft.Azure.WebJobs.Description;

namespace Extensions
{
    [Binding]
    [AttributeUsage(AttributeTargets.Parameter)]
    public class TestServiceAttribute : Attribute
    {
    }
}
```

But you can add your own parameters (into the constructor for example)

Now you can use your input binding in your function :D
```
using Extensions;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace FunctionApp3
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [TestService] ITestService testService, // <- right here
            ILogger log)
        {
            log.LogInformation(testService.Greet());
            return new OkObjectResult(testService.Greet());
        }
    }
}
```

## Is this supported by MS?
I have no idea but it seems to be the way that MS are registering their own input bindings: https://github.com/Azure/azure-webjobs-sdk-extensions/blob/master/src/WebJobs.Extensions.CosmosDB/CosmosDBWebJobsStartup.cs so I assume that this is just the way this happens.

## Any strange behaviour?
Yes! 

* For some reason your `IWebJobsStartup` class needs to be in a seperate project 
