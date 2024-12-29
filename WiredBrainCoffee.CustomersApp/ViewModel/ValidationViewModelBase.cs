using System.Collections;
using System.ComponentModel;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();
        public IEnumerable GetErrors(string? propertyName)
        {
            return propertyName is not null && _errorsByPropertyName.TryGetValue(propertyName, out var value)
                ? value
                : Enumerable.Empty<string>();
        }

        public bool HasErrors => _errorsByPropertyName.Any();
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }

        protected void AddError(string error, string propertyName)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = [];
            }

            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        protected void ClearErrors(string propertyName)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName)) return;

            _errorsByPropertyName.Remove(propertyName);
            OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
            RaisePropertyChanged(nameof(HasErrors));
        }
    }
}
