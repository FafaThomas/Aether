using Aether.Sync.Services;

namespace Aether.Sync.Workers;

public class GenreSyncWorker
{
    private readonly TmdbService _tmdb;
    private readonly PostgresService _postgres;

    public GenreSyncWorker(
        TmdbService tmdb,
        PostgresService postgres)
    {
        _tmdb = tmdb;
        _postgres = postgres;
    }

    public async Task SyncGenresAsync()
    {
        var genres =
            await _tmdb.GetGenresAsync();

        Console.WriteLine(
            $"Found {genres.Count} genres.");

        foreach (var genre in genres)
        {
            await _postgres.UpsertGenreAsync(genre);

            Console.WriteLine(
                $"Genre [{genre.Id}] {genre.Name}");
        }
    }
}