using Microsoft.UI.Xaml.Controls;

namespace Aether_UI.Controls;

public sealed partial class PlayerControl : UserControl
{
    public PlayerControl()
    {
        InitializeComponent();

        Play("https://www.vidking.net/embed/tv/259837/1/1");
    }

    public void Play(string url)
    {
        PlayerView.Source = new Uri(url);
    }
}