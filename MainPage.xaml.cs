using Octokit;
using System.Reflection;

namespace VersionControl
{
    public partial class MainPage : ContentPage
    {
        Microsoft.Maui.Controls.Label label;
       private async Task loadVersion()
        {
            string owner = "Paragalactica"; // Замените на имя владельца вашего репозитория
            string repo = "VersionControlTest"; // Замените на имя вашего репозитория

            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            GitHubClient github = new GitHubClient(new ProductHeaderValue("VersionControl"));
            var releases = await github.Repository.Release.GetAll(owner, repo);

            var latestRelease = releases.FirstOrDefault();
            if (latestRelease != null && latestRelease.TagName != currentVersion)
            {
                Console.WriteLine($"Доступна новая версия: {latestRelease.TagName}");

                label.Text = $"Обновление требуется. Доступна новая версия: {latestRelease.TagName}";
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
        }

        private async void versionCheckClick(object sender, EventArgs e)
        {
            await loadVersion();
        }
    }

}
