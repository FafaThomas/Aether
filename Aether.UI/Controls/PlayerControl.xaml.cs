using Microsoft.UI.Xaml.Controls;

namespace Aether_UI.Controls;

public sealed partial class PlayerControl : UserControl
{
    public PlayerControl()
    {
        InitializeComponent();

        Play("https://www.vidking.net/embed/movie/10193");
    }

    public void Play(string url)
    {
        PlayerView.Source = new Uri(url);
    }
}