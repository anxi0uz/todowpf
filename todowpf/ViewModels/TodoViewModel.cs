using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using todowpf.Abstractions;
using todowpf.Models;
using todowpf.Services;
using todowpf.Windows;

namespace todowpf.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Todo> todos = new ObservableCollection<Todo>();
        private readonly HttpClient _client;
        private readonly ITokenStorage storage;
        private readonly ISelectedTodoService todoService;
        private readonly INavigationService navigationService;

        public TodoViewModel(IHttpClientFactory factory, ITokenStorage storage, ISelectedTodoService todoService, INavigationService navigationService)
        {
            _client = factory.CreateClient("Api");
            this.storage = storage;
            this.todoService = todoService;
            this.navigationService = navigationService;
            GetTodos();
        }
        [RelayCommand]
        public async Task GetTodos()
        { 
             var todos = await _client.GetFromJsonAsync<List<Todo>>($"/todo/{storage.UserId}");
            if (todos.Any())
            {
                this.todos.Clear();
                foreach (var todo in todos)
                {
                    this.todos.Add(todo);
                }
            }
        }
        [RelayCommand]
        public async Task CreateTodo()
        {
            navigationService.Navigate<AddTodo, AddTodoViewModel>();
        }
        [RelayCommand]
        public async Task DoubleClick(object? parametr)
        {
            if (parametr is Todo todo)
            {
                todoService.Todo = todo;
                navigationService.Navigate<EditTodo, EditTodoViewModel>();   
            }
        }
        [RelayCommand]
        public async Task Logout()
        {
            storage.Clear();
            navigationService.Navigate<LoginWindow, LoginViewModel>();
        }
    }
}
