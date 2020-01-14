using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web;
using System.Web.Http;

namespace Cibertec.WebApi.App_Start
{
    public class WebApiConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config.MessageHandlers.Insert(0, new ServerCompressionHandler(
                new GZipCompressor(), new DeflateCompressor()));

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new
                CamelCasePropertyNamesContractResolver();
        }
    }
}