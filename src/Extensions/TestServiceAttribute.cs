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