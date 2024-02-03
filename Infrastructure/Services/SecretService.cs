using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Infrastructure.Services;

public static class SecretService
{
    private const string Url = "https://blogkeyvault.vault.azure.net/";

    public static string GetSecret(string secret)
    {
        var client = new SecretClient(new Uri(Url), new DefaultAzureCredential());
        try
        {
            var keySecret = (KeyVaultSecret)client.GetSecret(secret);
            var secretValue = keySecret.Value!;
            return secretValue;
        }
        catch (Exception ex)
        {
            throw new Exception($"ErrorsHandler retrieving secret: {ex.Message}");
        }
    }
}
