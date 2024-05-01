namespace UrlShortenerService.Console
{
    using UrlShortenerService.Library.Services;
    using System;
    using UrlShortenerService.Library.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using UrlShortenerService.Library.Interfaces;
    using Microsoft.Extensions.Configuration;

    class Program
    {
        static void Main(string[] args)
        {
            // Load configuration
            var configuration = UrlShortenerConfiguration.Load();

            // Set up DI container
            var serviceProvider = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton<IUrlMappingRepository, InMemoryUrlMappingRepository>()
                .AddSingleton<IKeyGenerationStrategy, RandomKeyGenerationStrategy>(sp => new RandomKeyGenerationStrategy(configuration.CharSet))
                .AddSingleton<UrlShorteningService>()
                .BuildServiceProvider();

            // Get an instance of UrlShortenerService
            var urlShortenerService = serviceProvider.GetRequiredService<UrlShorteningService>();

            new ConsoleUI(urlShortenerService).Start();
        }
    }
}