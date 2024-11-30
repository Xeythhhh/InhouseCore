using Carter;

using Microsoft.Extensions.Caching.Memory;

namespace Api.Endpoints;

public class StaticAssetsEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app) =>
        // Serve static files under the "/Assets" route
        app.MapGet("api/Assets/{game}/{*path}", async (HttpContext context, IMemoryCache cache, string game, string path) =>
        {
            string gameFolder = Path.Combine(AppContext.BaseDirectory, game);
            string filePath = Path.Combine(gameFolder, path);

            if (!game.StartsWith("Game-") || !Directory.Exists(gameFolder) || !File.Exists(filePath))
            {
                return Results.NotFound("File not found.");
            }

            // Use server-side cache
            string cacheKey = $"{game}/{path}";
            if (!cache.TryGetValue(cacheKey, out byte[]? fileBytes))
            {
                // todo log cache hits/misses

                fileBytes = await File.ReadAllBytesAsync(filePath);

                cache.Set(cacheKey, fileBytes, new MemoryCacheEntryOptions
                {
                    Size = fileBytes.Length, // Account for size in bytes
                    SlidingExpiration = TimeSpan.FromHours(24)
                });
            }

            // Set Cache-Control header for browser caching
            context.Response.Headers.CacheControl = "public, max-age=2592000"; // Cache for 30 days

            return Results.File(fileBytes!, GetMimeType(filePath));
        });

    // Helper to determine the MIME type based on file extension
    private static string GetMimeType(string fileName) =>
        Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".tga" => "image/x-targa",
            _ => "application/octet-stream", // Default to binary stream
        };
}