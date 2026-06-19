namespace Aether_UI.Models;

public class MovieCard
{
    public int TmdbId { get; set; }

    public string Title { get; set; } = "";

    public string PosterPath { get; set; } = "";

    public double VoteAverage { get; set; }

    public string PosterUrl =>
        $"https://image.tmdb.org/t/p/w342{PosterPath}";

    public string Overview { get; set; } = "";

    public string BackdropPath { get; set; } = "";

    public string BackdropUrl =>
        $"https://image.tmdb.org/t/p/w780{BackdropPath}";
}