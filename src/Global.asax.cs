using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OtelExporter
{
    public class MvcApplication : HttpApplication
    {
        private TracerProvider _tracerProvider;
        
        protected void Application_Start()
        {
            _tracerProvider = Sdk.CreateTracerProviderBuilder()
                .AddAspNetInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation()
                .AddOtlpExporter(options =>
                    {
                        options.Endpoint = new Uri($"http://localhost:4317");
                    })
                .AddSource("my-service-name")
                .SetResourceBuilder(
                    ResourceBuilder.CreateDefault()
                        .AddService(serviceName: "my-service-name", serviceVersion: "1.0.0"))

                .Build();
        }
        
        protected void Application_End()
        {
            _tracerProvider?.Dispose();
        }
    }
}