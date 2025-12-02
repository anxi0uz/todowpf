using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using todowpf.Services;
using todowpf.ViewModels;
using todowpf.Windows;

namespace todowpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddHttpClient<HttpClient>("Api",opt => opt.BaseAddress = new Uri("https://localhost:7068"));
            serviceCollection.AddSingleton<LoginWindow>();
            serviceCollection.AddSingleton<TodoWindow>();
            serviceCollection.AddTransient<IAuthService, AuthService>();
            serviceCollection.AddTransient<LoginViewModel>();
            serviceCollection.AddSingleton<ITokenStorage, InMemoryTokenStorage>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var login = ServiceProvider.GetRequiredService<LoginWindow>();
            login.Show();
            base.OnStartup(e);
        }
    }

}
