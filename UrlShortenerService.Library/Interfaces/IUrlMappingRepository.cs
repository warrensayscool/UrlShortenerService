namespace UrlShortenerService.Library.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using UrlShortenerService.Library.Models;

    public interface IUrlMappingRepository
    {
        void Add(UrlMapping urlMapping);
        UrlMapping Get(string shortUrl);
        bool Exists(string key);
    }
}
