using Shop.Domain.Models;

namespace Shop.Domain.ResponceModels;

public class RefreshResponceModel
{
	public string Jwt { get; set; } = string.Empty;
	public string Message { get; set; } = string.Empty;
	public RefreshToken RefreshToken { get; set; } = new RefreshToken();
}