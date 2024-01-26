using Octokit;
using System.Reflection;

namespace VersionControl
{
    public partial class MainPage : ContentPage
    {
       private async Task loadVersion()
        {
            string owner = "владелец"; // Замените на имя владельца вашего репозитория
            string repo = "репозиторий"; // Замените на имя вашего репозитория

            string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            GitHubClient github = new GitHubClient(new ProductHeaderValue("YourApp"));
            var releases = await github.Repository.Release.GetAll(owner, repo);

            var latestRelease = releases.FirstOrDefault();
            if (latestRelease != null && latestRelease.TagName != currentVersion)
            {
                Console.WriteLine($"Доступна новая версия: {latestRelease.TagName}");

                Console.WriteLine("Обновление требуется.");
            }
            else
            {
                Console.WriteLine("У вас последняя версия.");
            }
        }
        public MainPage()
        {
            InitializeComponent();
            
        }

        private async void versionCheckClick(object sender, EventArgs e)
        {
            await loadVersion();
        }
    }

}
