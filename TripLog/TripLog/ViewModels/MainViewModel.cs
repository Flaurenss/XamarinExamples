﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TripLogEntry> _logEntries;
        public ObservableCollection<TripLogEntry> LogEntries
        {
            get => _logEntries;
            set
            {
                _logEntries = value;
                OnPropertyChanged();
            }
        }

        public Command<TripLogEntry> ViewCommand => new Command<TripLogEntry>
            (async entry => await NavService.NavigateTo<DetailViewModel,TripLogEntry>(entry));

        public Command NewEntryCommand => new Command( async() => await NavService.NavigateTo<NewEntryViewModel>());

        public MainViewModel(INavService navService) : base(navService)
        {
            LogEntries = new ObservableCollection<TripLogEntry>();
        }

        public override void Init()
        {
            LogEntries.Clear();
            LogEntries.Add(new TripLogEntry
            {
                Title = "Washington Monument",
                Notes = "Amazing!",
                Rating = 3,
                Date = new DateTime(2019, 2, 5),
                Latitude = 38.8895,
                Longitude = -77.0352
            });
            LogEntries.Add(new TripLogEntry
            {
                Title = "Statue of Liberty",
                Notes = "Inspiring!",
                Rating = 4,
                Date = new DateTime(2019, 4, 13),
                Latitude = 40.6892,
                Longitude = -74.0444
            });
            LogEntries.Add(new TripLogEntry
            {
                Title = "Golden Gate Bridge",
                Notes = "Foggy, but beautiful.",
                Rating = 5,
                Date = new DateTime(2019, 4, 26),
                Latitude = 37.8268,
                Longitude = -122.4798
            });
        }
    }
}
