using System.Security.Cryptography;

namespace Shop.Domain.Models;

public class RefreshToken
{
	public string Token { get; set; } = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
	public DateTime Created { get; set; } = DateTime.UtcNow;
	public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);
}