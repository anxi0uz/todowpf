using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using todowpf.Abstractions;
using todowpf.Models;
using todowpf.Services;
using todowpf.Windows;

namespace todowpf.ViewModels
{
    public partial class AddTodoViewModel: ObservableObject
    {
        [ObservableProperty]
        private string _name = string.Empty;
        [ObservableProperty]
        private string _description = string.Empty;


        private HttpClient _client;
        private ITokenStorage _storage;
        private readonly INavigationService navigate;

        public AddTodoViewModel(IHttpClientFactory factory, ITokenStorage storage, INavigationService navigate)
        {
            _client = factory.CreateClient("Api");
            _storage = storage;
            this.navigate = navigate;
        }

        [RelayCommand]
        public async Task CreateTodo()
        {
            var todoRequest = new TodoRequest(_storage.UserId, _name,_description);
            var response = await _client.PostAsJsonAsync("/todo",todoRequest);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Успешно");
            }
            navigate.Navigate<TodoWindow, TodoViewModel>();
        }

    }
}
