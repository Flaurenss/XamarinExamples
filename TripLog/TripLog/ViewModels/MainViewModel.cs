using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TripLog.Models;

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
    }
}
