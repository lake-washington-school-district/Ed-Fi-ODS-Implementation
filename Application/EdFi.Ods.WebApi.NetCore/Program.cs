// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace EdFi.Ods.WebApi.NetCore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var _logger = LogManager.GetLogger(typeof(Program));
            _logger.Debug("Loading configuration files");

            ConfigureLogging();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostBuilderContext, configbuilder) =>
                    {
                        configbuilder.SetBasePath(hostBuilderContext.HostingEnvironment.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{hostBuilderContext.HostingEnvironment.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
                    })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).Build();

            await host.RunAsync();

            static void ConfigureLogging()
            {
                var assembly = typeof(Program).GetTypeInfo().Assembly;

                string configPath = Path.Combine(Path.GetDirectoryName(assembly.Location), "log4net.config");

                XmlConfigurator.Configure(LogManager.GetRepository(assembly), new FileInfo(configPath));
            }
        }
    }
}
