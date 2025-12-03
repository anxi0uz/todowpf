using System.Windows;

namespace todowpf.Abstractions
{
    public interface INavigationService
    {
        void Navigate<TView, TViewModel>()
            where TView : Window, new()
            where TViewModel : class;
    }
}