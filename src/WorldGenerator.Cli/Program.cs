using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using WorldGenerator.Cli.Arguments;
using WorldGenerator.Core;

using Random = WorldGenerator.Core.Random;

namespace WorldGenerator.Cli
{
    internal class Program
    {
        private static async Task Main(string[] args) => await BuildCommandLine()
            .UseHost(_ => Host.CreateDefaultBuilder(),
                host =>
                {
                    host.ConfigureServices(services =>
                    {
                        services.AddSingleton<IRandom, Random>();
                        services.AddTransient<RootCommandHandler>();
                        services.AddTransient<Core.WorldGenerator>();
                        services.AddTransient<AlgebraService>();
                        services.AddTransient<IDrawingFactory, SvgDrawingFactory>();
                    });
                })
            .UseDefaults()
            .Build()
            .InvokeAsync(args);

        private static CommandLineBuilder BuildCommandLine()
        {
            var root = new RootCommand()
            {
                new Option<int>("--points", () => 1000),
                new Option<int>("--width", () => 500),
                new Option<int>("--height", () => 500),
                new Option<int>("--relax", () => 2),
                new Option<int>("--seed"),
            };

            root.Handler = CommandHandler.Create<GeneratorArguments, IHost>((args, host) => Run<RootCommandHandler, GeneratorArguments>(args, host));
            return new CommandLineBuilder(root);
        }

        private static Task<int> Run<TCommandHandler, TArgs>(TArgs args, IHost host)
            where TCommandHandler : BaseCommandHandler<TArgs>
        {
            var serviceProvider = host.Services;
            var handler = serviceProvider.GetRequiredService<TCommandHandler>();

            return handler.ExecuteCommand(args);
        }
    }
}
