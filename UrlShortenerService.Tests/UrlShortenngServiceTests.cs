namespace UrlShortenerService.Library.Services.Tests
{
    using Moq;

    using UrlShortenerService.Library.Configuration;
    using UrlShortenerService.Library.Exceptions;
    using UrlShortenerService.Library.Interfaces;
    using UrlShortenerService.Library.Models;

    using Xunit;
    public class UrlShortenngServiceTests
    {
        private readonly Mock<IUrlMappingRepository> _repositoryMock;
        private readonly Mock<IKeyGenerationStrategy> _keyGenerationStrategyMock;
        private readonly UrlShortenerConfiguration _configuration;
        private readonly UrlShorteningService _urlShortenerService;

        public UrlShortenngServiceTests()
        {
            _repositoryMock = new Mock<IUrlMappingRepository>();
            _keyGenerationStrategyMock = new Mock<IKeyGenerationStrategy>();

            _configuration = new UrlShortenerConfiguration
            {
                BaseShortUrl = "https://syort.st/",
                CharSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789",
                KeyLength = 6
            };

            _urlShortenerService = new UrlShorteningService(_configuration, _repositoryMock.Object, _keyGenerationStrategyMock.Object);
        }

        [Fact]
        public void GenerateShortUrl_ShouldReturnShortUrl()
        {
            // Arrange
            string longUrl = "https://www.example.com/very/long/url";
            string generatedKey = "abc123";
            _keyGenerationStrategyMock.Setup(m => m.GenerateKey(It.IsAny<int>())).Returns(generatedKey);

            // Act
            string shortUrl = _urlShortenerService.GenerateShortUrl(longUrl);

            // Assert
            Assert.Equal($"{_configuration.BaseShortUrl}{generatedKey}", shortUrl);
            _repositoryMock.Verify(r => r.Add(It.Is<UrlMapping>(m => m.LongUrl == longUrl && m.ShortUrl == shortUrl)), Times.Once);
        }

        [Fact]
        public void GetLongUrl_ShouldReturnLongUrl()
        {
            // Arrange
            string longUrl = "https://www.example.com/very/long/url";
            string generatedKey = "abc123";
            string shortUrl = $"{_configuration.BaseShortUrl}{generatedKey}";
            UrlMapping urlMapping = new UrlMapping { Key = generatedKey, LongUrl = longUrl, ShortUrl = shortUrl };
            _repositoryMock.Setup(r => r.Exists(generatedKey)).Returns(true);
            _repositoryMock.Setup(r => r.Get(generatedKey)).Returns(urlMapping);

            // Act
            string retrievedLongUrl = _urlShortenerService.GetLongUrl(shortUrl);

            // Assert
            Assert.Equal(longUrl, retrievedLongUrl);
        }

        [Fact]
        public void GetLongUrl_ShouldThrowException_WhenShortUrlDoesNotExist()
        {
            // Arrange
            string invalidShortUrl = "https://syort.st/INVALID";
            _repositoryMock.Setup(r => r.Exists(It.IsAny<string>())).Returns(false);

            // Act & Assert
            Assert.Throws<UrlShortenerException>(() => _urlShortenerService.GetLongUrl(invalidShortUrl));
        }
    }
}