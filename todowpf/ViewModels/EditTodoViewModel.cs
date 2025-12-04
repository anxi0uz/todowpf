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
    public partial class EditTodoViewModel : ObservableObject
    {
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string name = string.Empty;
        [ObservableProperty]
        public string description = string.Empty;

        public INavigationService NavigationService { get; }
        public ISelectedTodoService SelectedTodo { get; }

        private HttpClient _client;

        public EditTodoViewModel(INavigationService navigationService, IHttpClientFactory factory, ISelectedTodoService selectedTodo)
        {
            NavigationService = navigationService;
            SelectedTodo = selectedTodo;
            _client = factory.CreateClient("Api");
            name = selectedTodo.Todo.Name;
            description = selectedTodo.Todo.Description;
            id = selectedTodo.Todo.Id;
        }

        [RelayCommand]
        public async Task UpdateTodo()
        {
            var model = new TodoRequest(SelectedTodo.Todo.UserId,name,description);
            var response = await _client.PutAsJsonAsync($"/todo/{SelectedTodo.Todo.Id}",model);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Успешно обновлено!");
            }
            NavigationService.Navigate<TodoWindow, TodoViewModel>();
        }

        [RelayCommand]
        public async Task DeleteTodo()
        {
            var response = await _client.DeleteAsync($"/todo/{SelectedTodo.Todo.Id}");
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Успешно удалено!");
            }
            NavigationService.Navigate<TodoWindow, TodoViewModel>();
        }
    }
}
