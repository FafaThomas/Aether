using System.Text.Json.Serialization;

namespace Aether.Sync.Models;

public class GenreResponse
{
    [JsonPropertyName("genres")]
    public List<Genre> Genres { get; set; } = [];
}