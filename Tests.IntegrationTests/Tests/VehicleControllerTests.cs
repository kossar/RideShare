using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using API.DTO.v1.Models.Identity;
using API.DTO.v1.Models.Vehicle;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Factories;
using Tests.IntegrationTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.Tests;

public class VehicleControllerTests : IClassFixture<VehicleWebApplicationFactory<Program>>
{
    private readonly VehicleWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    private string? _token = null;
    public VehicleControllerTests(VehicleWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
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

    [Fact]
    public async Task Vehicles_GetAll_ShouldBeUnAuthorized()
    {
        //ARRANGE
        var uri = "api/v1/Vehicles";

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }


    [Fact]
    public async Task Vehicles_GetAll_ShouldReturnVehicles()
    {
        //ARRANGE
        var uri = "api/v1/Vehicles";
        await GetToken();

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<IEnumerable<VehicleModel>>(body);

        Assert.NotNull(data);
        Assert.True(data?.Any());
    }

    [Fact]
    public async Task Vehicles_Add_ShouldAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/Vehicles";

        var dto = new CreateUpdateVehicleModel
        {
            Make = "BMW",
            Model = "X¤",
            Number = "FFF000"
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var data = await response.GetResultAsync<VehicleModel>();

        Assert.NotNull(data);
        Assert.True(data.Id != Guid.Empty);
        Assert.True(data.UserId != Guid.Empty);
        Assert.Equal(dto.Make, data?.Make);
        Assert.Equal(dto.Model, data?.Model);
        Assert.Equal(dto.Number, data?.Number);
    }

    [Fact]
    public async Task Vehicles_Add_ShouldAddAndGetByID()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/Vehicles";

        var dto = new CreateUpdateVehicleModel
        {
            Make = "BMW",
            Model = "X¤",
            Number = "FFF000"
        };

        // ACT
       
        var response = await _client.PostAsJsonAsync(uri, dto);
        response.EnsureSuccessStatusCode();
        var data = await response.GetResultAsync<VehicleModel>();
        Assert.NotNull(data);


        var responseGetById = await _client.GetAsync($"{uri}/{data?.Id}");

        var dataGetById = await responseGetById.GetResultAsync<VehicleModel>();
        //ASSERT
        responseGetById.EnsureSuccessStatusCode();

        Assert.Equal(dto.Make, dataGetById?.Make);
        Assert.Equal(dto.Model, dataGetById?.Model);
        Assert.Equal(dto.Number, dataGetById?.Number);
    }

    [Fact]
    public async Task Vehicles_Add_ShouldNotAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/Vehicles";

        var dto = new CreateUpdateVehicleModel
        {
            Make = "BMW",
            Model = "X¤",
            Number = null
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