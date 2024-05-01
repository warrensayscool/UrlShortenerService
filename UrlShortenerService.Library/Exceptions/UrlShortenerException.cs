using System;

namespace UrlShortenerService.Library.Exceptions
{
    public class UrlShortenerException : Exception
    {
        public UrlShortenerException(string message) : base(message)
        {
        }
    }
}