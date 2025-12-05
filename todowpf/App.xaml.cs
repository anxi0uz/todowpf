using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Windows;
using todowpf.Abstractions;
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
            serviceCollection
                .AddHttpClient<HttpClient>("Api", opt => opt.BaseAddress = new Uri("https://localhost:7068"))
                .AddHttpMessageHandler<AuthorizationMessageHandler>(); ;
            serviceCollection.AddSingleton<LoginWindow>();
            serviceCollection.AddTransient<AuthorizationMessageHandler>();
            serviceCollection.AddSingleton<TodoWindow>();
            serviceCollection.AddTransient<IAuthService, AuthService>();
            serviceCollection.AddTransient<TodoViewModel>();
            serviceCollection.AddTransient<LoginViewModel>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<ITokenStorage, InMemoryTokenStorage>();
            serviceCollection.AddSingleton<AddTodo>();
            serviceCollection.AddSingleton<ISelectedTodoService, SelectedTodoService>();
            serviceCollection.AddSingleton<EditTodo>();
            serviceCollection.AddTransient<EditTodoViewModel>();
            serviceCollection.AddTransient<AddTodoViewModel>();
            ServiceProvider = serviceCollection.BuildServiceProvider();
            //var login = ServiceProvider.GetRequiredService<LoginWindow>();
            //login.Show();
            var nav = ServiceProvider.GetService<INavigationService>();
            nav.Navigate<LoginWindow, LoginViewModel>();
            base.OnStartup(e);
        }
    }

}
