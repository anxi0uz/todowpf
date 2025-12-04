using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using todowpf.Abstractions;
using todowpf.Models;
using todowpf.Services;
using todowpf.Windows;

namespace todowpf.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly INavigationService navigationService;
        [ObservableProperty]
        private string _username = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        public LoginViewModel(IAuthService authService, ITokenStorage storage, TodoWindow window, INavigationService navigationService)
        {
            AuthService = authService;
            Storage = storage;
            Window = window;
            this.navigationService = navigationService;
        }

        public IAuthService AuthService { get; }
        public ITokenStorage Storage { get; }
        public TodoWindow Window { get; }

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                if (string.IsNullOrEmpty(_username))
                {
                    MessageBox.Show("Введите имя пользователя!");
                    return;
                }
                if (string.IsNullOrEmpty(_password))
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }
                var model = new AuthRequest(_password, _username);
                var response = await AuthService.Login(model);
                if (response != null)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(response.AccessToken);
                    jwtToken.Payload.TryGetValue("userid", out var userid);
                    Storage.UserId = Convert.ToInt32(userid);
                    Storage.Token = response.AccessToken;
                    navigationService.Navigate<TodoWindow, TodoViewModel>();
                }
                else
                {
                    MessageBox.Show("Ошибка авторизации");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка братуха: {ex.Message}");
            }
        }


        
    }
}


