namespace UrlShortenerService.Library.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UrlMapping
    {
        public string Key { get; set; } = string.Empty;
        public string LongUrl { get; set; } = string.Empty;
        public string ShortUrl { get; set; } = string.Empty;
    }
}
