using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add HttpClient for external API calls
builder.Services.AddHttpClient();

// Add CORS for development
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

// GET /me endpoint
app.MapGet("/me", async (IHttpClientFactory httpClientFactory) =>
{
    var httpClient = httpClientFactory.CreateClient();
    httpClient.Timeout = TimeSpan.FromSeconds(5);

    string catFact = "Cats are amazing creatures!"; // Fallback fact

    try
    {
        var response = await httpClient.GetAsync("https://catfact.ninja/fact");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var catFactResponse = JsonSerializer.Deserialize<CatFactResponse>(content);

            if (catFactResponse?.fact != null)
            {
                catFact = catFactResponse.fact;
            }
        }
    }
    catch (Exception ex)
    {
        // Log error but continue with fallback fact
        Console.WriteLine($"Error fetching cat fact: {ex.Message}");
    }

    var profileResponse = new
    {
        status = "success",
        user = new
        {
            email = "herdeybayor4real@gmail.com",
            name = "Sherifdeen Adebayo",
            stack = ".NET/ASP.NET Core"
        },
        timestamp = DateTime.UtcNow.ToString("o"), // ISO 8601 format
        fact = catFact
    };

    return Results.Json(profileResponse, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    });
})
.Produces(200)
.WithName("GetProfile");

app.MapGet("/", () => "Hello World");

app.MapFallback(() => "No route found");

app.Run();

// Record for deserializing Cat Fact API response
record CatFactResponse(string fact, int length);
