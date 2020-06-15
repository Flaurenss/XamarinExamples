using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using TripLog.Models;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseValidationViewModel
    {
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                Validate(() => !string.IsNullOrWhiteSpace(_title),"Title must be provided");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }
        private double _latitude;
        public double Latitude
        {
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }
        private double _longitude;
        public double Longitude
        {
            get => _longitude;
            set
            {
                _longitude = value;
                OnPropertyChanged();
            }
        }
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        private int _rating;
        public int Rating
        {
            get => _rating;
            set
            {
                _rating = value;
                Validate(() => _rating >= 1 && _rating <= 5,"Rating must be between 1 and 5");
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }
        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        private Command _saveCommand;
        //if _saveCommand == null then execute right part
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(Save, CanSave));

        public NewEntryViewModel()
        {
            Date = DateTime.Today;
            Rating = 1;
        }

        private void Save()
        {
            var newItem = new TripLogEntry()
            {
                Title = Title,
                Latitude = Latitude,
                Longitude = Longitude,
                Date = Date,
                Rating = Rating,
                Notes = Notes
            };
            //TODO implement persist entry
        }
        //CanSave determinates if the command can be executed or not
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;
    }
}
