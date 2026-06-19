using Microsoft.UI.Xaml.Controls;
using DotNetEnv;

using Aether_UI.Services;
using Aether_UI.Models;
using Aether.Core.Services;
using Aether.Core.Workers;

//For Optimization Purposes
using System.Diagnostics;


namespace Aether_UI;

public sealed partial class MainPage : Page
{
    private string _connectionString = "";

    public MainPage()
    {
        InitializeComponent();

        Loaded += MainPage_Loaded;
    }

    private async void MainPage_Loaded(
        object sender,
        Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        //For Optimization Purposes
        var sw = Stopwatch.StartNew();
        try
        {
            Env.TraversePath().Load();

            _connectionString =
                $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
                $"Database={Environment.GetEnvironmentVariable("POSTGRES_DB")};" +
                $"Username={Environment.GetEnvironmentVariable("POSTGRES_USER")};" +
                $"Password={Environment.GetEnvironmentVariable("POSTGRES_PASSWORD")};";

            await LoadMoviesAsync();

            await RunSyncAsync();

            await LoadMoviesAsync();
        }
        catch (Exception ex)
        {
            Content = new TextBlock
            {
                Text = ex.ToString()
            };
        }

        //For Optimization Purposes
        Console.WriteLine(
        $"Total Startup: {sw.ElapsedMilliseconds} ms");
    }

    private async Task RunSyncAsync()
    {
        string apiKey =
            Environment.GetEnvironmentVariable("TMDB_API_KEY")!;

        var tmdb =
            new TmdbService(apiKey);

        var postgres =
            new PostgresService(_connectionString);

        var genreWorker =
            new GenreSyncWorker(
                tmdb,
                postgres);

        var movieWorker =
            new MovieSyncWorker(
                tmdb,
                postgres);

        var detailWorker =
            new DetailSyncWorker(
                tmdb,
                postgres);

        var coordinator =
            new SyncCoordinator(
                genreWorker,
                movieWorker,
                detailWorker,
                postgres);

        await coordinator.RunAsync();
    }

    private async Task LoadMoviesAsync()
    {
        var catalog =
            new CatalogService(_connectionString);

        MoviesGrid.ItemsSource =
            await catalog.GetTop20MoviesAsync();
    }

    private void MoviesGrid_ItemClick(
    object sender,
    ItemClickEventArgs e)
    {
    var movie =
        (MovieCard)e.ClickedItem;

    Frame.Navigate(
        typeof(MovieDetailsPage),
        movie);
    }

}