using DotNetEnv;
using Aether.Sync.Services;
using Aether.Sync.Workers;



Env.Load("../.env");



Console.WriteLine($"Current Directory: {Directory.GetCurrentDirectory()}");

Console.WriteLine($"../.env Exists: {File.Exists("../.env")}");

Env.Load("../.env");

string apiKey =
    Environment.GetEnvironmentVariable("TMDB_API_KEY")!;

string connectionString =
    $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
    $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
    $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
    $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";

var tmdb =
    new TmdbService(apiKey);

var postgres =
    new PostgresService(connectionString);

var worker =
    new MovieSyncWorker(tmdb, postgres);


var genreWorker =
    new GenreSyncWorker(
        tmdb,
        postgres);


var detailWorker =
    new DetailSyncWorker(
        tmdb,
        postgres);




await genreWorker.SyncGenresAsync();
await worker.SyncPopularMoviesAsync(1);
await detailWorker.SyncMovieDetailsAsync();

Console.WriteLine("Sync complete.");