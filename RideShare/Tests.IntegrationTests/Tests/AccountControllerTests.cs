using System.Net;
using System.Net.Http.Json;
using API.DTO.v1;
using API.DTO.v1.Models.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Factories;
using Tests.IntegrationTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.Tests;

public class AccountControllerTests : IClassFixture<AccountWebApplicationFactory<Program>>
{
    private readonly AccountWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    public AccountControllerTests(AccountWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("test_database_name", "test_db");
            })
            .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
    }


    #region Register

    [Fact]
    public async Task Register_ShouldRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test1@test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        var body = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(response.StatusCode.ToString());
        var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(data?.Token);
        Assert.Equal(dto.Firstname, data?.Firstname);
        Assert.Equal(dto.Lastname, data?.Lastname);
    }

    [Fact]
    public async Task Register_WithNonAlphaNumericPassword_ShouldRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test2@test.ee",
            Password = "T.est123",
            PasswordConfirmation = "T.est123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        var body = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(response.StatusCode.ToString());
        var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(data?.Token);
        Assert.Equal(dto.Firstname, data?.Firstname);
        Assert.Equal(dto.Lastname, data?.Lastname);
    }

    [Fact]
    public async Task Register_WithLongPassword_ShouldRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test3@test.ee",
            Password = "T.est123T.est123T.est123T.est123",
            PasswordConfirmation = "T.est123T.est123T.est123T.est123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        var body = await response.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(response.StatusCode.ToString());
        var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);

        response.EnsureSuccessStatusCode();
        Assert.NotNull(data?.Token);
        Assert.Equal(dto.Firstname, data?.Firstname);
        Assert.Equal(dto.Lastname, data?.Lastname);
    }



    [Fact]
    public async Task Register_EmailExists_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var firstDto = new Register
        {
            Email = "testFirst@test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };

        var secondDto = new Register
        {
            Email = "testFirst@test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test 2",
            Lastname = "Kasutaja 2"
        };
        // ACT
        var firstResponse = await _client.PostAsJsonAsync(uri, firstDto);
        var responseWithDuplicate = await _client.PostAsJsonAsync(uri, secondDto);

        // ASSERT
        var body = await firstResponse.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(firstResponse.StatusCode.ToString());
        var firstResponseData = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
        firstResponse.EnsureSuccessStatusCode();

        Assert.NotNull(firstResponseData?.Token);
        Assert.Equal(firstDto.Firstname, firstResponseData?.Firstname);
        Assert.Equal(firstDto.Lastname, firstResponseData?.Lastname);

        Assert.Equal(HttpStatusCode.BadRequest, responseWithDuplicate.StatusCode);
    }

    [Fact]
    public async Task Register_NoUpperCaseInPassword_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test5@test.ee",
            Password = "test123",
            PasswordConfirmation = "test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    [Fact]
    public async Task Register_NoLowerCaseInPassword_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test6@test.ee",
            Password = "TEST123",
            PasswordConfirmation = "TEST123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_NoNumberInPassword_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test7@test.ee",
            Password = "Testiiiii",
            PasswordConfirmation = "Testiiiii",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_PasswordNotMatching_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test8@test.ee",
            Password = "Test122",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_TooShortPassword_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test9@test.ee",
            Password = "Test12",
            PasswordConfirmation = "Test12",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_NoFirstName_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test10@test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = string.Empty,
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_NoLastName_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test11@test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = string.Empty
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_InCorrectEmail_WithoutAt_ShouldNotRegister()
    {
        // ARRANGE 
        var uri = "api/v1/Account/Register";

        var dto = new Register
        {
            Email = "test12test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        // ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    #endregion

    #region Login

    [Fact]
    public async Task Login_ExistingUser_ShouldLogIn()
    {
        // ARRANGE 
        var registerUri = "api/v1/Account/Register";

        var registerDto = new Register
        {
            Email = "test14@test.ee",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };

        var loginUri = "api/v1/Account/Login";

        var loginDto = new Login()
        {
            Email = "test14@test.ee",
            Password = "Test123"
        };
        // ACT
        var registerResponse = await _client.PostAsJsonAsync(registerUri, registerDto);

        var loginResponse = await _client.PostAsJsonAsync(loginUri, loginDto);

        // ASSERT
        registerResponse.EnsureSuccessStatusCode();


        var body = await loginResponse.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(loginResponse.StatusCode.ToString());
        var loginResponseData = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);

        loginResponse.EnsureSuccessStatusCode();
        Assert.NotNull(loginResponseData?.Token);
        Assert.Equal(registerDto.Firstname, loginResponseData?.Firstname);
        Assert.Equal(registerDto.Lastname, loginResponseData?.Lastname);
    }

    [Fact]
    public async Task Login_WrongEmail_ShouldNotLogIn()
    {

        // ARRANGE 
        var registerUri = "api/v1/Account/Register";

        var registerDto = new Register
        {
            Email = "test15@test",
            Password = "Test123",
            PasswordConfirmation = "Test123",
            Firstname = "Test",
            Lastname = "Kasutaja"
        };

        var loginUri = "api/v1/Account/Login";

        var loginDto = new Login()
        {
            Email = "test15@test",
            Password = "Test123"
        };
        // ACT
        var registerResponse = await _client.PostAsJsonAsync(registerUri, registerDto);

        var loginResponse = await _client.PostAsJsonAsync(loginUri, loginDto);

        // ASSERT
        registerResponse.EnsureSuccessStatusCode();


        var body = await loginResponse.Content.ReadAsStringAsync();
        _testOutputHelper.WriteLine(loginResponse.StatusCode.ToString());
        var loginResponseData = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
    }
    #endregion
}