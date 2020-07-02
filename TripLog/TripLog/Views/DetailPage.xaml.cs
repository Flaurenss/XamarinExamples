using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using TripLog.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TripLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        //Get access to values from our BindingContext in that case our ViewModel
        private DetailViewModel ViewModel => BindingContext as DetailViewModel;
        public DetailPage()
        {
            InitializeComponent();
        }

        private void UpdateMap()
        {
            if(ViewModel.Entry == null)
            {
                return;
            }
            //Center map on given position from entry data
            map.MoveToRegion(MapSpan.FromCenterAndRadius(
            new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude), Distance.FromMiles(.5)));
            //place a pin on the map
            map.Pins.Add(new Pin
            {
                Type = PinType.Place,
                Label = ViewModel.Entry.Title,
                Position = new Position(ViewModel.Entry.Latitude, ViewModel.Entry.Longitude)
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            if(ViewModel != null)
            {
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if(ViewModel != null)
            {
                ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
            }
        }
        /// <summary>
        /// Method prompt by the PropertyChanged event that will fire up when Entry (data needed for the map )
        /// changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if(args.PropertyName == nameof(DetailViewModel.Entry))
            {
                UpdateMap();
            }
        }
    }
}