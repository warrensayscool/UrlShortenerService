namespace UrlShortenerService.Library.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IKeyGenerationStrategy
    {
        string GenerateKey(int length);
    }
}
