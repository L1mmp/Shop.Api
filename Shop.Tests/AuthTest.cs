using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Shop.Application.Interfaces;
using Shop.DataAccess.Repositories.Interfaces;
using Shop.Domain.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Mappings;
using Shop.Infrastructure.Providers;
using Shop.Infrastructure.Services;

namespace Shop.Tests;

[TestFixture]
public class AuthTest
{
	private IConfiguration _configuration;
	public AuthTest()
	{
		var environment = Environment.GetEnvironmentVariable("Development");

		var dict = new Dictionary<string, string>
		{
			{"AppSettings:Token", "TopSecurityTokenQwe"}
		};

		_configuration = new ConfigurationManager().AddInMemoryCollection(dict).Build();
	}

	[Test]
	public async Task AuthTest_ShouldReturnSuccessAuth()
	{
		//arrange

		var mapper = new Mapper(new MapperConfiguration(x => x.AddProfile(new EntityDtosProfile())));
		var userRepo = new Mock<IUserRepository>();
		var cartRepo = new Mock<ICartRepository>();
		var http = new Mock<IHttpContextAccessor>();
		var tokenProvider = new TokenProvider(_configuration);
		var serivce = new AuthService(_configuration, mapper, 
			userRepo.Object, cartRepo.Object, http.Object, tokenProvider);

		var userDto = new UserDto()
		{
			Name = "test",
			Age = 20,
			Login = "test",
			Password = "test"
		};

		//act

		var result = await serivce.Register(userDto);

		//assert

		Assert.IsNotEmpty(result.Token);
	}

	[Test]
	public async Task AuthTest_ShouldThrowException()
	{
		//arrange
		var tokenProvider = new TokenProvider(_configuration);
		var mapper = new Mapper(new MapperConfiguration(x => x.AddProfile(new EntityDtosProfile())));
		var userRepo = new Mock<IUserRepository>();
		var cartRepo = new Mock<ICartRepository>();
		var http = new Mock<IHttpContextAccessor>();
		var serivce = new AuthService(_configuration, mapper,
			userRepo.Object, cartRepo.Object, http.Object, tokenProvider);

		var userDto = new UserDto()
		{
			Name = "test",
			Age = 20,
			Login = "test",
			Password = "test"
		};

		//act

		await serivce.Register(userDto);
		var result = await serivce.Register(userDto);

		var users = await userRepo.Object.GetAllAsync();

		//assert

	}

}