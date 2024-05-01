namespace UrlShortenerService.Library.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using UrlShortenerService.Library.Interfaces;

    public class RandomKeyGenerationStrategy : IKeyGenerationStrategy
    {
        private readonly string _chars;
        private readonly Random _random;

        public RandomKeyGenerationStrategy(string chars)
        {
            _chars = chars;
            _random = new Random();
        }

        public string GenerateKey(int length)
        {
            return new string(Enumerable.Repeat(_chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}
