using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tests.IntegrationTests.Helpers;

public static class HttpClientExtensions

{

    public static async Task<HttpResponseMessage> GetAsync(this HttpClient httpClient, string url, bool ensure = false, CancellationToken cancellationToken = default)

    {

        var response = await httpClient.GetAsync(url, cancellationToken);

        if (ensure)

        {

            response.EnsureSuccessStatusCode();

        }

        return response;

    }

    public static async Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string url, CancellationToken cancellationToken = default)

    {

        return await httpClient.PostAsync(url, null, cancellationToken);

    }



    public static async Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string url, object request, bool ensure = false, CancellationToken cancellationToken = default)

    {

        var response = await httpClient.PostAsync(url, GetJsonContent(request), cancellationToken);

        if (ensure)

        {

            response.EnsureSuccessStatusCode();

        }

        return response;

    }



    public static async Task<HttpResponseMessage> PutAsync(this HttpClient httpClient, string url, object request, bool ensure = false, CancellationToken cancellationToken = default)

    {

        var response = await httpClient.PutAsync(url, GetJsonContent(request), cancellationToken);

        if (ensure)

        {

            response.EnsureSuccessStatusCode();

        }

        return response;

    }



    public static async Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string url, object request, bool ensure = false, CancellationToken cancellationToken = default)

    {

        var response = await httpClient.PatchAsync(url, GetJsonContent(request), cancellationToken);

        if (ensure)

        {

            response.EnsureSuccessStatusCode();

        }

        return response;

    }



    public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string url, string stringContent, CancellationToken cancellationToken = default)

    {

        var req = new HttpRequestMessage(HttpMethod.Delete, url);

        req.Content = new StringContent(stringContent, Encoding.UTF8, "application/json");



        var response = await httpClient.SendAsync(req, cancellationToken);

        return response;

    }



    public static async Task<TRes> GetResultAsync<TRes>(this HttpResponseMessage res,

        CancellationToken cancellationToken = default)

    {

        await using var stream = await res.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<TRes>(

            stream,

            new JsonSerializerOptions

            {

                PropertyNameCaseInsensitive = true,

                Converters = { new JsonStringEnumConverter() }

            },

            cancellationToken: cancellationToken

        );

    }



    private static StringContent GetJsonContent<T>(T request)

    {

        return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

    }

}