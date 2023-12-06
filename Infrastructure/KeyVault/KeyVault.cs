using Microsoft.Extensions.Configuration;

namespace Infrastructure.KeyVault;

public class KeyVault
{
    public IConfiguration _Configuration;

    public KeyVault(string key)
    {
        Key = _Configuration[$"{key}"]!;
    }

    public string Key { get; }
}
