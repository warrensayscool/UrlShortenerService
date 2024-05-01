namespace UrlShortenerService.Console
{
    using System;
    using System.Text.RegularExpressions;

    using UrlShortenerService.Library.Services;
    public class ConsoleUI
    {
        private readonly UrlShorteningService _urlShorteningService;

        public ConsoleUI(UrlShorteningService urlShorteningService)
        {
            _urlShorteningService = urlShorteningService;
        }

        public void Start()
        {
            Console.WriteLine("Welcome to the URL Shortener Service!");

            while (true)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Generate Short URL");
                Console.WriteLine("2. Get Long URL");
                Console.WriteLine("3. Exit");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        GenerateShortUrl();
                        break;
                    case "2":
                        GetLongUrl();
                        break;
                    case "3":
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void GenerateShortUrl()
        {
            Console.Write("Enter the long URL: ");
            string? longUrl = Console.ReadLine();

            if (IsValidUrl(longUrl))
            {
                try
                {
                    string shortUrl = _urlShorteningService.GenerateShortUrl(longUrl);
                    Console.WriteLine($"Short URL: {shortUrl}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid URL format. Please enter a valid URL.");
            }
        }

        private void GetLongUrl()
        {
            Console.Write("Enter the short URL: ");
            string? shortUrl = Console.ReadLine();

            if (IsValidShortUrl(shortUrl))
            {
                try
                {
                    string longUrl = _urlShorteningService.GetLongUrl(shortUrl);
                    Console.WriteLine($"Long URL: {longUrl}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid short URL format. Please enter a valid short URL.");
            }
        }

        private bool IsValidShortUrl(string? shortUrl)
        {
            if (string.IsNullOrEmpty(shortUrl))
            {
                return false;
            }
            string pattern = @"^[a-zA-Z0-9\-_\.~:/\?#\[\]@!\$&'\(\)\*\+,;=%]+$";

            return Regex.IsMatch(shortUrl, pattern);
        }

        private bool IsValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            return Uri.TryCreate(url, UriKind.Absolute, out var result)
                   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
