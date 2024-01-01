using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace Blog_Api.Services;

public class KeyVault
{
    private const string Url = "https://blogkeyvault.vault.azure.net/";

    public string GetSecret(string secret)
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
            throw new Exception($"Error retrieving secret: {ex.Message}");
        }
    }
}
