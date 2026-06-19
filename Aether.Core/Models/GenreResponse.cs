using System.Text.Json.Serialization;

namespace Aether.Core.Models;

public class GenreResponse
{
    [JsonPropertyName("genres")]
    public List<Genre> Genres { get; set; } = [];
}