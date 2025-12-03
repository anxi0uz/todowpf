using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using todowpf.Abstractions;

namespace todowpf.Services
{
    public class NavigationService : INavigationService
    {
        private Window? _current;

        public void Navigate<TView, TViewModel>()
            where TView : Window, new()
            where TViewModel : class
        {
            _current?.Close();
            var vm = App.ServiceProvider.GetRequiredService<TViewModel>();
            var window = new TView { DataContext = vm };
            _current = window;
            window.Show();
        }
    }
}
