using Microsoft.Extensions.Configuration;

namespace UrlShortenerService.Library.Configuration
{
    public class UrlShortenerConfiguration
    {
        public string BaseShortUrl { get; set; }
        public string CharSet { get; set; }
        public int KeyLength { get; set; }

        public static UrlShortenerConfiguration Load()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);

            IConfiguration configuration = builder.Build();
            var urlShortenerConfiguration = new UrlShortenerConfiguration();
            configuration.GetSection("UrlShortenerConfiguration").Bind(urlShortenerConfiguration);

            return urlShortenerConfiguration;
        }
    }
}