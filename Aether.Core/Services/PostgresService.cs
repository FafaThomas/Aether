using Aether.Core.Models;
using Dapper;
using Npgsql;

namespace Aether.Core.Services;

public class PostgresService
{
    private readonly string _connectionString;

    public PostgresService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task UpsertDimMovieAsync(Movie movie)
    {
        const string sql = """
        INSERT INTO aether.dim_movies
        (
            tmdb_id,
            title,
            original_title,
            release_date,
            poster_path,
            backdrop_path
        )
        VALUES
        (
            @Id,
            @Title,
            @OriginalTitle,
            @ReleaseDate,
            @PosterPath,
            @BackdropPath
        )
        ON CONFLICT (tmdb_id)
        DO UPDATE SET
            title = EXCLUDED.title,
            original_title = EXCLUDED.original_title,
            release_date = EXCLUDED.release_date,
            poster_path = EXCLUDED.poster_path,
            backdrop_path = EXCLUDED.backdrop_path,
            updated_at = NOW();
        """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(sql, movie);
    }

    public async Task UpsertMovieDiscoveryAsync(
        int tmdbId,
        string source)
    {
        const string sql = """
        INSERT INTO aether.fact_movie_discovery
        (
            tmdb_id,
            source
        )
        VALUES
        (
            @TmdbId,
            @Source
        )
        ON CONFLICT (tmdb_id)
        DO NOTHING;
        """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(
            sql,
            new
            {
                TmdbId = tmdbId,
                Source = source
            });
    }

    public async Task InsertPopularitySnapshotAsync(
        int tmdbId,
        string source,
        int page,
        int rank,
        double popularity)
    {
        const string sql = """
        INSERT INTO aether.fact_popularity_snapshots
        (
            tmdb_id,
            source,
            snapshot_date,
            page_number,
            rank_position,
            popularity
        )
        VALUES
        (
            @TmdbId,
            @Source,
            CURRENT_DATE,
            @Page,
            @Rank,
            @Popularity
        );
        """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(
            sql,
            new
            {
                TmdbId = tmdbId,
                Source = source,
                Page = page,
                Rank = rank,
                Popularity = popularity
            });
    }

    public async Task UpsertMovieGenreAsync(
        int tmdbId,
        int genreId)
    {
        const string sql = """
        INSERT INTO aether.bridge_movie_genres
        (
            tmdb_id,
            genre_id
        )
        VALUES
        (
            @TmdbId,
            @GenreId
        )
        ON CONFLICT
        DO NOTHING;
        """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        await connection.ExecuteAsync(
            sql,
            new
            {
                TmdbId = tmdbId,
                GenreId = genreId
            });
    }

    public async Task UpsertGenreAsync(Genre genre)
    {
    const string sql = """
    INSERT INTO aether.genres
    (
        genre_id,
        name
    )
    VALUES
    (
        @Id,
        @Name
    )
    ON CONFLICT (genre_id)
    DO UPDATE SET
        name = EXCLUDED.name;
    """;

    await using var connection =
        new NpgsqlConnection(_connectionString);

    await connection.ExecuteAsync(sql, genre);
    }

    public async Task UpsertMovieDetailsAsync(
    MovieDetailsResponse details)
    {
    const string sql = """
    INSERT INTO aether.fact_movie_details
    (
        tmdb_id,
        overview,
        popularity,
        vote_average,
        vote_count,
        runtime,
        last_synced
    )
    VALUES
    (
        @Id,
        @Overview,
        @Popularity,
        @VoteAverage,
        @VoteCount,
        @Runtime,
        NOW()
    )
    ON CONFLICT (tmdb_id)
    DO UPDATE SET
        overview = EXCLUDED.overview,
        popularity = EXCLUDED.popularity,
        vote_average = EXCLUDED.vote_average,
        vote_count = EXCLUDED.vote_count,
        runtime = EXCLUDED.runtime,
        last_synced = NOW();
    """;

    await using var connection =
        new NpgsqlConnection(_connectionString);

    await connection.ExecuteAsync(
        sql,
        details);
    }

    public async Task<List<int>>
    GetMoviesMissingDetailsAsync()
    {
    const string sql = """
    SELECT dm.tmdb_id
    FROM aether.dim_movies dm
    LEFT JOIN aether.fact_movie_details fd
        ON dm.tmdb_id = fd.tmdb_id
    WHERE fd.tmdb_id IS NULL
    ORDER BY dm.tmdb_id;
    """;

    await using var connection =
        new NpgsqlConnection(_connectionString);

    var result =
        await connection.QueryAsync<int>(sql);

    return result.ToList();
    }

    public async Task<DateOnly?> GetLastSyncDateAsync(
    string syncName)
    {
    const string sql = """
    SELECT last_sync_date
    FROM aether.sync_state
    WHERE sync_name = @SyncName;
    """;

    await using var connection =
        new NpgsqlConnection(_connectionString);

    return await connection.QueryFirstOrDefaultAsync<DateOnly?>(
        sql,
        new { SyncName = syncName });
    }

    public async Task UpdateLastSyncDateAsync(
    string syncName)
    {
    const string sql = """
    INSERT INTO aether.sync_state
    (
        sync_name,
        last_sync_date
    )
    VALUES
    (
        @SyncName,
        CURRENT_DATE
    )
    ON CONFLICT (sync_name)
    DO UPDATE SET
        last_sync_date = CURRENT_DATE;
    """;

    await using var connection =
        new NpgsqlConnection(_connectionString);

    await connection.ExecuteAsync(
        sql,
        new { SyncName = syncName });
    }
}