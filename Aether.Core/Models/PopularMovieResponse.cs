using System.Text.Json.Serialization;

namespace Aether.Core.Models;

public class PopularMoviesResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }

    [JsonPropertyName("results")]
    public List<Movie> Results { get; set; } = [];
}