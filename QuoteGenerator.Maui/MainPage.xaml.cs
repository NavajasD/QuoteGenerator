namespace QuoteGenerator.Maui
{
    public partial class MainPage : ContentPage
    {
        private Random random = new Random();
        private List<string> quotes = new List<string>();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GenerateNewBackground();
            await LoadQuotesFromMauiAsset();
        }

        private void buttonGenerateQuote_Clicked(object sender, EventArgs e)
        {
            GenerateNewBackground();
            if (quotes.Count > 0)
                labelQuote.Text = quotes[
                    random.Next(
                        quotes.Count)];
        }

        private async Task LoadQuotesFromMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("quotes.txt");
            using var reader = new StreamReader(stream);

            while (reader.Peek() != -1)
                quotes.Add(reader.ReadLine() ?? "");
        }

        private void GenerateNewBackground()
        {
            var startColor = System.Drawing.Color.FromArgb(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));

            var endColor = System.Drawing.Color.FromArgb(
                random.Next(0, 256),
                random.Next(0, 256),
                random.Next(0, 256));

            var colors = ColorControls.GetColorGradient(
                startColor,
                endColor,
                6);

            float stopOffset = .0f;
            GradientStopCollection stops = new GradientStopCollection();

            colors.ForEach(c => {
                stops.Add(new GradientStop(Color.FromArgb(c.Name), stopOffset));
                stopOffset += .2f;
            });

            LinearGradientBrush gradient = new LinearGradientBrush(
                stops,
                new Point(0, 0),
                new Point(1, 1));

            gridBackground.Background = gradient;
        }
    }

}
