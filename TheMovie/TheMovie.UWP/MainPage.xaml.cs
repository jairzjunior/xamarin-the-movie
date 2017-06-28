namespace TheMovie.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new TheMovie.App());
        }
    }
}