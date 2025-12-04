using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using todowpf.Abstractions;
using todowpf.Windows;

namespace todowpf.ViewModels
{
    public partial class DeleteTodoViewModel : ObservableObject
    {
        [ObservableProperty]
        public int id;

        public DeleteTodoViewModel(IHttpClientFactory factory, INavigationService navigationService)
        {
            NavigationService = navigationService;
            _client = factory.CreateClient("Api");
        }

        public INavigationService NavigationService { get; }

        private HttpClient _client;
        [RelayCommand]
        public async Task DeleteTodo()
        {
            var result = await _client.DeleteAsync($"/todo/{id}");
            if (result.IsSuccessStatusCode)
            {
                MessageBox.Show("Удалено");
            }
            NavigationService.Navigate<TodoWindow, TodoViewModel>();
        }

    }
}
