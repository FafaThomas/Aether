using System.Net.Http.Json;
using Aether.Sync.Models;

namespace Aether.Sync.Services;

public class TmdbService
{
    private readonly HttpClient _httpClient = new();
    private readonly string _apiKey;

    public TmdbService(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<PopularMoviesResponse>
        GetPopularMoviesAsync(int page)
    {
        string url =
            $"https://api.themoviedb.org/3/movie/popular?api_key={_apiKey}&page={page}";

        var response =
            await _httpClient.GetFromJsonAsync<PopularMoviesResponse>(url);

        return response!;
    }

    public async Task<List<Genre>> GetGenresAsync()
    {
    string url =
        $"https://api.themoviedb.org/3/genre/movie/list?api_key={_apiKey}";

    var response =
        await _httpClient.GetFromJsonAsync<GenreResponse>(url);

    return response?.Genres ?? [];
    }

    public async Task<MovieDetailsResponse>
    GetMovieDetailsAsync(int tmdbId)
    {
    string url =
        $"https://api.themoviedb.org/3/movie/{tmdbId}?api_key={_apiKey}";

    var response =
        await _httpClient.GetFromJsonAsync<MovieDetailsResponse>(url);

    return response!;
    }
}