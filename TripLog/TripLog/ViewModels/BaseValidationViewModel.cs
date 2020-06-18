using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TripLog.Services;

namespace TripLog.ViewModels
{
    public class BaseValidationViewModel : BaseViewModel, INotifyDataErrorInfo
    {
        readonly IDictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public bool HasErrors => _errors?.Any(x => x.Value?.Any() == true) == true;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public BaseValidationViewModel(INavService navService) : base(navService)
        {

        }

        public IEnumerable GetErrors(string propertyName)
        {
            if(string.IsNullOrWhiteSpace(propertyName))
            {
                return _errors.SelectMany(x => x.Value);
            }

            if(_errors.ContainsKey(propertyName) && _errors[propertyName].Any())
            {
                return _errors[propertyName];
            }

            return new List<string>();
        }
        /// <summary>
        /// Validation action adding the error to our dictionary in case it fails
        /// </summary>
        /// <param name="validationRule"></param>
        /// <param name="errorMessage"></param>
        /// <param name="propertyName">If we dont pass the propertyname it will be fill auto</param>
        protected void Validate(Func<bool> validationRule, string errorMessage, [CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName)) return;
            if (_errors.ContainsKey(propertyName)) _errors.Remove(propertyName);
            //in case our validation rule isnt true that means that we have an error
            if(validationRule() != true)
            {
                _errors.Add(propertyName, new List<string>{ errorMessage });
            }
            var tmp = nameof(HasErrors);
            OnPropertyChanged(tmp);
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
