using Octokit;
using System.Reflection;

namespace VersionControl
{
    public partial class MainPage : ContentPage
    {
        Microsoft.Maui.Controls.Label label;
        Microsoft.Maui.Controls.Label labelCo;
        private async Task loadVersion()
        {
            string owner = "Paragalactica"; // Замените на имя владельца вашего репозитория
            string repo = "VersionControlTest"; // Замените на имя вашего репозитория

            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            GitHubClient github = new GitHubClient(new ProductHeaderValue("VersionControl"));
            github.Credentials = new Credentials("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");

            var releases = await github.Repository.Release.GetAll(owner, repo);
            //foreach (var item in releases)
            //{
            //    labelCo.Text = item.Name;
            //}

            var latestRelease = releases.FirstOrDefault();

            DisplayAlert("Последняя версия", releases + "", "Ок");
            DisplayAlert("Текущая версия", currentVersion + "", "Ок");

            //DisplayAlert("Последняя версия", latestRelease.TagName + "", "Ок");

            if (latestRelease != null && latestRelease.TagName != currentVersion)
            {
                Console.WriteLine($"Доступна новая версия: {latestRelease.TagName}");

                label.Text = $"Обновление требуется. Доступна новая версия: {latestRelease.TagName}";
                DisplayAlert("Последняя версия", latestRelease + "", "Ок");
            }
            else
            {
                label.Text = "У вас последняя версия.";
                
            }
        }
        public MainPage()
        {
            InitializeComponent();
            label = this.Update;
            labelCo = this.Control;
        }

        private async void versionCheckClick(object sender, EventArgs e)
        {
            await loadVersion();
        }
    }

}
