using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewEntryPage : ContentPage
    {
        NewEntryViewModel ViewModel => BindingContext as NewEntryViewModel;
        public NewEntryPage()
        {
            InitializeComponent();
            BindingContextChanged += Page_BindignContextChanged;
        }

        private void Page_BindignContextChanged(object sender, EventArgs e)
        {
            ViewModel.ErrorsChanged += ViewModel_ErrorsChanged;
        }

        private void ViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            var hasErrors = (ViewModel.GetErrors(e.PropertyName) as List<string>)?.Any() == true;
            switch(e.PropertyName)
            {
                case nameof(ViewModel.Title):
                    title.LabelColor = hasErrors ? Color.Red : Color.Black;
                    break;
                case nameof(ViewModel.Rating):
                    rating.LabelColor = hasErrors ? Color.Red : Color.Black;
                    break;
                default:
                    break;
            }
        }
    }
}