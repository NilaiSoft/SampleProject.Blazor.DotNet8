using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.Globalization;

namespace SampleProjects.Server
{
    public static class CustomMidlware
    {
        public static void MyMidlware(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var cultureQuery = context.Request.Query["culture"];
                if (!string.IsNullOrWhiteSpace(cultureQuery))
                {
                    var culture = new CultureInfo(cultureQuery);

                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }

                var remoteIp = context.Connection.RemoteIpAddress;

                //------------------------------------------------
                //Get Target URL From Secondary File Requests
                var path = context.Request.Path;
                var referer = context.Request.Headers["Referer"];
                var url = context.Request.GetDisplayUrl();
                context.TraceIdentifier = Guid.NewGuid().ToString();
                //------------------------------------------------


                await next(context);
            });
        }
    }
}
