using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using Aether_UI.Models;

namespace Aether_UI;

public sealed partial class PlayerPage : Page
{
    public PlayerPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(
        NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        var movie =
            (MovieCard)e.Parameter;

        PlayerWebView.Source =
            new Uri(
                $"https://www.vidking.net/embed/movie/{movie.TmdbId}");
    }
}