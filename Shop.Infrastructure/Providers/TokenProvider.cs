using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shop.Domain.Entities;
using Shop.Domain.Models;

namespace Shop.Infrastructure.Providers;

public class TokenProvider
{
	private readonly IConfiguration _configuration;

	public TokenProvider(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public string CreateToken(User user)
	{
		var claims = new List<Claim>()
		{
			new Claim(ClaimTypes.Name, user.Login),
			new Claim("Id", user.Id.ToString()),
			new Claim("cartId",user.Cart.Id.ToString())
		};

		var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
			_configuration.GetSection("AppSettings:Token").Value));

		var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			claims: claims,
			expires: DateTime.UtcNow.AddDays(1),
			signingCredentials: creds);

		var jwt = new JwtSecurityTokenHandler().WriteToken(token);

		return jwt;
	}

	public RefreshToken CreateRefreshToken()
	{
		var token = new RefreshToken();

		return token;
	}


}