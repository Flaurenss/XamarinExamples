using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TripLog.Models;
using TripLog.Services;
using Xamarin.Forms;

namespace TripLog.ViewModels
{
    public class NewEntryViewModel : BaseValidationViewModel
    {
        #region Properties
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
        #endregion
        private Command _saveCommand;
        //if _saveCommand == null then execute right part
        public Command SaveCommand => _saveCommand ?? (_saveCommand = 
            new Command(async () => await Save(), CanSave));

        public NewEntryViewModel(INavService navService) : base(navService)
        {
            Date = DateTime.Today;
            Rating = 1;
        }

        public override void Init()
        {
        }

        private async Task Save()
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
            await NavService.GoBack();
        }
        //CanSave determinates if the command can be executed or not
        bool CanSave() => !string.IsNullOrWhiteSpace(Title) && !HasErrors;
    }
}
