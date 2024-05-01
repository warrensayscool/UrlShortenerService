namespace UrlShortenerService.Library.Services
{
    using Microsoft.Extensions.Configuration;

    using UrlShortenerService.Library.Configuration;
    using UrlShortenerService.Library.Exceptions;
    using UrlShortenerService.Library.Interfaces;
    using UrlShortenerService.Library.Models;

    public class UrlShorteningService
    {
        private readonly UrlShortenerConfiguration _configuration;
        private readonly string _baseShortUrl;
        private readonly IKeyGenerationStrategy _keyGenerationStrategy;
        private readonly IUrlMappingRepository _repository;

        public UrlShorteningService(UrlShortenerConfiguration configuration, IUrlMappingRepository repository, IKeyGenerationStrategy keyGenerationStrategy)
        {
            _configuration = configuration;
            _baseShortUrl = configuration.BaseShortUrl;
            _repository = repository;
            _keyGenerationStrategy = keyGenerationStrategy;
        }

        public string GenerateShortUrl(string longUrl)
        {
            string key;
            int maxAttempts = 10; // Set a maximum number of attempts to generate a unique key
            int attempts = 0;

            do
            {
                key = _keyGenerationStrategy.GenerateKey(_configuration.KeyLength);
                attempts++;

                if (attempts > maxAttempts)
                {
                    throw new UrlShortenerException("Unable to generate a unique key after multiple attempts.");
                }
            } while (_repository.Exists(key));

            string shortUrl = $"{_baseShortUrl}{key}";

            UrlMapping urlMapping = new UrlMapping
            {
                Key = key,
                LongUrl = longUrl,
                ShortUrl = shortUrl
            };

            _repository.Add(urlMapping);

            return shortUrl;
        }

        public string GetLongUrl(string shortUrl)
        {
            // Extract the key from the short URL
            string key = shortUrl.Replace(_baseShortUrl, string.Empty);

            // Check if the key exists in the dictionary
            if (_repository.Exists(key))
            {
                return _repository.Get(key).LongUrl;
            }

            // Throw an exception if the short URL doesn't exist
            throw new UrlShortenerException($"Short URL '{shortUrl}' doesn't exist.");
        }
    }
}