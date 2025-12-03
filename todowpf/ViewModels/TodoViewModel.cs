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
using todowpf.Models;
using todowpf.Services;

namespace todowpf.ViewModels
{
    public partial class TodoViewModel : ObservableObject
    {
        [ObservableProperty]
        public ObservableCollection<Todo> todos = new ObservableCollection<Todo>();
        private readonly HttpClient _client;
        private readonly ITokenStorage _storage;

        public TodoViewModel(IHttpClientFactory factory, ITokenStorage storage)
        {
            _client = factory.CreateClient("Api");
            this._storage = storage;
            GetTodos();
        }
        [RelayCommand]
        public async Task GetTodos()
        { 
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _storage.Token);
             var todos = await _client.GetFromJsonAsync<List<Todo>>("/todo/1");
            if (todos.Any())
            {
                foreach (var todo in todos)
                {
                    this.todos.Add(todo);
                }
            }
        }
    }
}
