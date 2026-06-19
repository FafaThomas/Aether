using Aether.Sync.Services;

namespace Aether.Sync.Workers;

public class MovieSyncWorker
{
    private readonly TmdbService _tmdb;
    private readonly PostgresService _postgres;

    public MovieSyncWorker(
        TmdbService tmdb,
        PostgresService postgres)
    {
        _tmdb = tmdb;
        _postgres = postgres;
    }

    public async Task SyncPopularMoviesAsync(int page)
    {
        var response =
            await _tmdb.GetPopularMoviesAsync(page);

        for (int i = 0; i < response.Results.Count; i++)
        {
            var movie =
                response.Results[i];

            int rank =
                ((page - 1) * 20) + i + 1;

            await _postgres.UpsertDimMovieAsync(movie);

            await _postgres.UpsertMovieDiscoveryAsync(
                movie.Id,
                "popular");

            await _postgres.InsertPopularitySnapshotAsync(
                movie.Id,
                "popular",
                page,
                rank,
                movie.Popularity);

            foreach (var genreId in movie.GenreIds)
            {
                await _postgres.UpsertMovieGenreAsync(
                    movie.Id,
                    genreId);
            }

            Console.WriteLine(
                $"Rank {rank}: {movie.Title}");
        }
    }
}