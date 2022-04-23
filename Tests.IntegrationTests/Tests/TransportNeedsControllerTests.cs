using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using API.DTO.v1.Models.Identity;
using API.DTO.v1.Models.TransportNeed;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Data;
using Tests.IntegrationTests.Factories;
using Tests.IntegrationTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.Tests;

public class TransportNeedsControllerTests : IClassFixture<TransportNeedWebApplicationFactory<Program>>
{
    private readonly TransportNeedWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    private string? _token = null;
    public TransportNeedsControllerTests(TransportNeedWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("test_database_name", "transport_need_db");
            })
            .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
    }

    [Fact]
    public async Task TransportNeeds_GetAll_ShouldNotBeUnAuthorized()
    {
        //ARRANGE
        var uri = "api/v1/TransportNeeds";

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        response.EnsureSuccessStatusCode();
    }


    //[Fact]
    //public async Task TransportNeeds_GetAll_ShouldReturnTransportNeeds()
    //{
    //    //ARRANGE
    //    var uri = "api/v1/TransportNeeds";
    //    await GetToken();

    //    // ACT
    //    var response = await _client.GetAsync(uri);

    //    //ASSERT
    //    response.EnsureSuccessStatusCode();
    //    var body = await response.Content.ReadAsStringAsync();
    //    var data = JsonHelper.DeserializeWithWebDefaults<IEnumerable<TransportNeedAddModel>>(body);

    //    Assert.NotNull(data);
    //    Assert.Equal(4, data?.Count());
    //}

    [Fact]
    public async Task TransportNeeds_Add_ShouldAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/TransportNeeds";

        var dto = new TransportNeedAddModel
        {
            StartLocationId = TestConstants.TransportNeedStartLocationId,
            DestinationLocationId = TestConstants.TransportNeedDestinationLocationId,
            PersonCount = 1,
            Price = 2,
            IsAd = true,
            Description = null
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var data = await response.GetResultAsync<TransportNeedModel>();

        Assert.NotNull(data);
        Assert.True(data.Id != Guid.Empty);
        Assert.True(data.UserId != Guid.Empty);
        Assert.Equal(dto.StartLocationId, data?.StartLocationId);
        Assert.Equal(dto.DestinationLocationId, data?.DestinationLocationId);
        Assert.Equal(dto.PersonCount, data?.PersonCount);
        Assert.Equal(dto.IsAd, data?.IsAd);
        Assert.Equal(dto.Description, data?.Description);
    }

    [Fact]
    public async Task TransportNeeds_Add_ShouldAddAndGetByID()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/TransportNeeds";

        var dto = new TransportNeedAddModel
        {
            StartLocationId = TestConstants.TransportNeedStartLocationId,
            DestinationLocationId = TestConstants.TransportNeedDestinationLocationId,
            PersonCount = 1,
            Price = 2,
            IsAd = true,
            Description = null
        };

        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);
        response.EnsureSuccessStatusCode();
        var data = await response.GetResultAsync<TransportNeedModel>();

        var responseGetById = await _client.GetAsync($"{uri}/{data.Id}");
        responseGetById.EnsureSuccessStatusCode();
        var dataGetById = await responseGetById.GetResultAsync<TransportNeedModel>();

        //ASSERT


        Assert.NotNull(data);
        Assert.True(dataGetById.Id != Guid.Empty);
        Assert.True(dataGetById.UserId != Guid.Empty);
        Assert.Equal(dto.StartLocationId, dataGetById?.StartLocationId);
        Assert.Equal(dto.DestinationLocationId, dataGetById?.DestinationLocationId);
        Assert.Equal(dto.PersonCount, dataGetById?.PersonCount);
        Assert.Equal(dto.IsAd, dataGetById?.IsAd);
        Assert.Equal(dto.Description, dataGetById?.Description);
    }

    [Fact]
    public async Task TransportNeeds_Add_ShouldNotAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/TransportNeeds";

        var dto = new TransportNeedAddModel
        {
            StartLocationId = TestConstants.TransportNeedStartLocationId,
            PersonCount = 1,
            Price = 2,
            IsAd = true,
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