﻿using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using API.DTO.v1.Models.Identity;
using API.DTO.v1.Models.Location;
using API.DTO.v1.Models.TransportOffer;
using API.DTO.v1.Models.Vehicle;
using Microsoft.AspNetCore.Mvc.Testing;
using Tests.IntegrationTests.Data;
using Tests.IntegrationTests.Factories;
using Tests.IntegrationTests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace Tests.IntegrationTests.Tests;

public class TransportOfferControllerTests : IClassFixture<TransportOfferWebApplicationFactory<Program>>
{
    private readonly TransportOfferWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _testOutputHelper;

    private string? _token = null;
    public TransportOfferControllerTests(TransportOfferWebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _client = factory
            .WithWebHostBuilder(builder =>
            {
                builder.UseSetting("test_database_name", "transport_offer_db");
            })
            .CreateClient(new WebApplicationFactoryClientOptions()
                {
                    // dont follow redirects
                    AllowAutoRedirect = false
                }
            );
    }

    [Fact]
    public async Task TransportOffers_GetAll_ShouldNotBeUnAuthorized()
    {
        //ARRANGE
        var uri = "api/v1/TransportOffers";

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }


    //[Fact]
    public async Task TransportOffers_GetAll_ShouldReturnTransportOffers()
    {
        //ARRANGE
        var uri = "api/v1/TransportOffers";
        await GetToken();

        // ACT
        var response = await _client.GetAsync(uri);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var data = JsonHelper.DeserializeWithWebDefaults<IEnumerable<CreateUpdateTransportOfferModel>>(body);

        Assert.NotNull(data);
        Assert.Equal(4, data?.Count());
    }

    [Fact]
    public async Task TransportOffers_Add_ShouldAdd()
    {
        //ARRANGE
        await GetToken();
        var uri = "api/v1/TransportOffers";

        var dto = new CreateUpdateTransportOfferModel()
        {
            StartLocation = new CreateUpdateLocationModel()
            {
                Address = "xxx",
                City = "Tallinn",
                Country = "Eesti",
            },
            DestinationLocation = new CreateUpdateLocationModel()
            {
                Address = "ggg",
                City = "hhh",
                Country = "Eesti",
            },
            Vehicle = new CreateUpdateVehicleModel()
            {
                Number = "hhhh",
            },
            AvailableSeatCount = 1,
            Price = 10,
            IsAd = true,
            Description = null
        };
        // ACT
        var response = await _client.PostAsJsonAsync(uri, dto);

        //ASSERT
        response.EnsureSuccessStatusCode();
        var data = await response.GetResultAsync<TransportOfferModel>();

        Assert.NotNull(data);
        Assert.True(data.Id != Guid.Empty);
        Assert.True(data.UserId != Guid.Empty);
        //Assert.Equal(dto.StartLocationId, data?.StartLocationId);
        //Assert.Equal(dto.DestinationLocationId, data?.DestinationLocationId);
        //Assert.Equal(dto.VehicleId, data?.VehicleId);
        Assert.Equal(dto.AvailableSeatCount, data?.AvailableSeatCount);
        Assert.Equal(dto.Price, data?.Price);
        Assert.Equal(dto.IsAd, data?.IsAd);
        Assert.Equal(dto.Description, data?.Description);
    }

    //[Fact]
    //public async Task TransportOffers_Add_ShouldAddAndGetByID()
    //{
    //    //ARRANGE
    //    await GetToken();
    //    var uri = "api/v1/TransportOffers";

    //    var dto = new TransportOfferAddModel
    //    {
    //        Make = "BMW",
    //        Model = "X¤",
    //        Number = "FFF000"
    //    };

    //    // ACT

    //    var response = await _client.PostAsJsonAsync(uri, dto);
    //    response.EnsureSuccessStatusCode();
    //    var data = await response.GetResultAsync<TransportOfferModel>();
    //    Assert.NotNull(data);


    //    var responseGetById = await _client.GetAsync($"{uri}/{data?.Id}");

    //    var dataGetById = await responseGetById.GetResultAsync<TransportOfferModel>();
    //    //ASSERT
    //    responseGetById.EnsureSuccessStatusCode();

    //    Assert.Equal(dto.Make, dataGetById?.Make);
    //    Assert.Equal(dto.Model, dataGetById?.Model);
    //    Assert.Equal(dto.Number, dataGetById?.Number);
    //}

    //[Fact]
    //public async Task TransportOffers_Add_ShouldNotAdd()
    //{
    //    //ARRANGE
    //    await GetToken();
    //    var uri = "api/v1/TransportOffers";

    //    var dto = new TransportOfferAddModel
    //    {
    //        Make = "BMW",
    //        Model = "X¤",
    //        Number = null
    //    };
    //    // ACT
    //    var response = await _client.PostAsJsonAsync(uri, dto);

    //    //ASSERT
    //    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    //}



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