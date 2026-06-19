using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

using Aether_UI.Models;

namespace Aether_UI;

public sealed partial class MovieDetailsPage : Page
{
    private MovieCard? _movie;

    public MovieDetailsPage()
    {
        InitializeComponent();
    }

    protected override void OnNavigatedTo(
        NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);

        _movie =
            (MovieCard)e.Parameter;

        TitleText.Text =
            _movie.Title;

        RatingText.Text =
            $"⭐ {_movie.VoteAverage:F1}";

        OverviewText.Text =
            _movie.Overview;

        BackdropImage.Source =
            new BitmapImage(
                new Uri(_movie.BackdropUrl));
    }

    private void PlayButton_Click(
        object sender,
        Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        Frame.Navigate(
            typeof(PlayerPage),
            _movie);
    }
}