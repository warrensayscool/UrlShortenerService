namespace UrlShortenerService.Library.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using UrlShortenerService.Library.Interfaces;
    using UrlShortenerService.Library.Models;

    public class InMemoryUrlMappingRepository : IUrlMappingRepository
    {
        private readonly Dictionary<string, UrlMapping> _urlMappings;

        public InMemoryUrlMappingRepository()
        {
            _urlMappings = new Dictionary<string, UrlMapping>();
        }

        public void Add(UrlMapping urlMapping)
        {
            _urlMappings[urlMapping.Key] = urlMapping;
        }

        public UrlMapping Get(string shortUrl)
        {
            if (_urlMappings.TryGetValue(shortUrl, out var urlMapping))
            {
                return urlMapping;
            }

            return null;
        }

        public bool Exists(string key)
        {
            return _urlMappings.ContainsKey(key);
        }
    }
}
