using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TripLog.ViewModels;

namespace TripLog.Services
{
    public interface INavService
    {
        bool CanGoBack { get; }
        Task GoBack();
        Task NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateTo<TViewModel, TParameter>(TParameter param) where TViewModel : BaseViewModel;
        void RemoveLastView();
        void ClearBackStack();
        void NavigateToUri(Uri uri);

        event PropertyChangedEventHandler CanGoBackChanged;
    }
}
