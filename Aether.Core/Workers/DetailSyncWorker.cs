using Aether.Core.Services;

namespace Aether.Core.Workers;

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
            try
            {
                var details =
                    await _tmdb.GetMovieDetailsAsync(tmdbId);

                if (details == null)
                    continue;

                await _postgres.UpsertMovieDetailsAsync(details);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Failed {tmdbId}: {ex.Message}");
            }
        }
    }
}