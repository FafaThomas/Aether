using Dapper;
using Npgsql;
using Aether_UI.Models;

namespace Aether_UI.Services;

public class CatalogService
{
    private readonly string _connectionString;

    public CatalogService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<MovieCard>> GetTop20MoviesAsync()
    {
        const string sql = """
        SELECT
            dm.tmdb_id       AS TmdbId,
            dm.title         AS Title,
            dm.poster_path   AS PosterPath,
            dm.backdrop_path AS BackdropPath,

            fd.overview      AS Overview,
            fd.vote_average  AS VoteAverage

        FROM aether.dim_movies dm
        JOIN aether.fact_movie_details fd
            ON fd.tmdb_id = dm.tmdb_id

        ORDER BY fd.popularity DESC
        LIMIT 200;
        """;

        await using var connection =
            new NpgsqlConnection(_connectionString);

        var movies =
            await connection.QueryAsync<MovieCard>(sql);

        return movies.ToList();
    }
}