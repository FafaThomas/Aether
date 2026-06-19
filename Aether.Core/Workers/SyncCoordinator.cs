//For Optimization Purposes
using System.Diagnostics;

using Aether.Core.Services;

namespace Aether.Core.Workers;

public class SyncCoordinator
{
    private readonly GenreSyncWorker _genreWorker;
    private readonly MovieSyncWorker _movieWorker;
    private readonly DetailSyncWorker _detailWorker;
    private readonly PostgresService _postgres;

    public SyncCoordinator(
        GenreSyncWorker genreWorker,
        MovieSyncWorker movieWorker,
        DetailSyncWorker detailWorker,
        PostgresService postgres)
    {
        _genreWorker = genreWorker;
        _movieWorker = movieWorker;
        _detailWorker = detailWorker;
        _postgres = postgres;
    }

    public async Task RunAsync()
    {
        var lastSync =
            await _postgres.GetLastSyncDateAsync(
                "popular_movies");

        if (lastSync ==
            DateOnly.FromDateTime(DateTime.Today))
        {
            Console.WriteLine(
                "Popular movie catalog already synced today.");

            return;
        }

        //For Optimization Purposes
        var sw = Stopwatch.StartNew();

        Console.WriteLine("Syncing Genres...");
        await _genreWorker.SyncGenresAsync();

        Console.WriteLine("Syncing Popular Movies...");

        for (int page = 1; page <= 10; page++)
        {
            Console.WriteLine(
                $"Syncing Page {page}");

            await _movieWorker
                .SyncPopularMoviesAsync(page);
        }

        //For Optimization Purposes
        var detailSw = Stopwatch.StartNew();

        Console.WriteLine(
            "Syncing Movie Details...");

        await _detailWorker
            .SyncMovieDetailsAsync();

        await _postgres.UpdateLastSyncDateAsync(
            "popular_movies");

        Console.WriteLine("Sync Complete.");

        //For Optimization Purposes
        Console.WriteLine(
            $"Details: {detailSw.ElapsedMilliseconds} ms");

        Console.WriteLine(
            $"Total Sync: {sw.ElapsedMilliseconds} ms");
    }
}