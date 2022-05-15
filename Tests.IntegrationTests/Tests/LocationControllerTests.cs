using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using API.DTO.v1.Models.Identity;
using API.DTO.v1.Models.Location;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Factories;
using Tests.IntegrationTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.Tests;

public class LocationControllerTests : IClassFixture<LocationWebApplicationFactory<Program>>
{
    private readonly LocationWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    private string? _token = null;
    public LocationControllerTests(LocationWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("test_database_name", Guid.NewGuid().ToString());
            })
            .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );

    }

    [Fact]
    public async Task Locations_GetAll_ShouldBeUnAuthorized()
    {
        //ARRANGE
        var uri = "api/v1/Locations";

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }


    [Fact]
    public async Task Locations_GetAll_ShouldReturnLocations()
    {
        //ARRANGE
        var uri = "api/v1/Locations";
        await GetToken();

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<IEnumerable<CreateUpdateLocationModel>>(body);

        Assert.NotNull(data);
        Assert.Equal(5, data?.Count());
    }

    [Fact]
    public async Task Locations_Add_ShouldAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/Locations";

        var dto = new CreateUpdateLocationModel
        {
            Country = "Eesti",
            Province = "Harjumaa",
            City = "Tallinn",
            Address = "Adr",
            Description = null
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<CreateUpdateLocationModel>(body);

        Assert.NotNull(data);
        Assert.NotNull(data.Id);
        Assert.Equal(dto.Country, data?.Country);
        Assert.Equal(dto.Province, data?.Province);
        Assert.Equal(dto.City, data?.City);
        Assert.Equal(dto.Address, data?.Address);
    }

    [Fact]
    public async Task Locations_Add_ShouldAddAndGetByID()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/Locations";

        var dto = new CreateUpdateLocationModel
        {
            Country = "Eesti",
            Province = "Harjumaa",
            City = "Tallinn",
            Address = "Adr 22",
            Description = null
        };

        // ACT
       
        var response = await _client.PostAsJsonAsync(uri, dto);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<CreateUpdateLocationModel>(body);
        Assert.NotNull(data);


        var responseGetById = await _client.GetAsync($"{uri}/{data?.Id}");
        var bodyGetById = await responseGetById.Content.ReadAsStringAsync();
        var dataGetById = JsonHelper.DeserializeWithWebDefaults<CreateUpdateLocationModel>(bodyGetById);
        //ASSERT
        responseGetById.EnsureSuccessStatusCode();

        Assert.NotNull(dataGetById);
        Assert.NotNull(dataGetById?.Id);
        Assert.Equal(dto.Country, dataGetById?.Country);
        Assert.Equal(dto.Province, dataGetById?.Province);
        Assert.Equal(dto.City, dataGetById?.City);
        Assert.Equal(dto.Address, dataGetById?.Address);
    }

    [Fact]
    public async Task Locations_Add_ShouldNotAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/Locations";

        var dto = new CreateUpdateLocationModel
        {
            Province = "Harjumaa",
            City = "Tallinn",
            Description = null
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        //ASSERT
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }



    private async Task GetToken()
    {
        if (_token != null)
        {
            return;
        }
        var uri = "api/v1/Account/Login";

        var dto = new Login
        {
            Email = "test@user2.ee",
            Password = "Test123"
        };
        var response = await _client.PostAsJsonAsync(uri, dto);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<JwtResponse>(body);
        _token = data?.Token;
        _client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _token);
    }

}