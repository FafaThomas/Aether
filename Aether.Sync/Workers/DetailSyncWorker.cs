using Aether.Sync.Services;

namespace Aether.Sync.Workers;

public class DetailSyncWorker
{
    private readonly TmdbService _tmdb;
    private readonly PostgresService _postgres;

    public DetailSyncWorker(
        TmdbService tmdb,
        PostgresService postgres)
    {
        _tmdb = tmdb;
        _postgres = postgres;
    }

    public async Task SyncMovieDetailsAsync()
    {
        var movieIds =
            await _postgres.GetMoviesMissingDetailsAsync();

        Console.WriteLine(
            $"Found {movieIds.Count} movies needing enrichment.");

        foreach (var tmdbId in movieIds)
        {
            var details =
                await _tmdb.GetMovieDetailsAsync(tmdbId);

            await _postgres.UpsertMovieDetailsAsync(details);

            Console.WriteLine(
                $"Enriched [{tmdbId}] Runtime={details.Runtime}");
        }
    }
}