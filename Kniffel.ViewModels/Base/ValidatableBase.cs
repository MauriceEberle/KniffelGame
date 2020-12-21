using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kniffel.ViewModels.Base
{
    
    public abstract class ValidatableBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> errorsByPropertyName = new Dictionary<string, List<string>>();

        private readonly Dictionary<string, Guid> lastValidationProcess = new Dictionary<string, Guid>();

        private readonly Dictionary<string, Func<Task<List<string>>>> validatorsAsync = new Dictionary<string, Func<Task<List<string>>>>();

        private readonly Dictionary<string, Func<List<string>>> validators = new Dictionary<string, Func<List<string>>>();


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private bool isValidating;
        private bool isValid;

        public bool IsValidating
        {
            get { return isValidating; }
            set
            {
                isValidating = value;
                OnPropertyChanged();
            }
        }

        public bool IsValid
        {
            get { return isValid; }
            set
            {
                isValid = value;
                OnPropertyChanged();
            }
        }

        protected ValidatableBase()
        {
            PropertyChanged += (sender, args) => CheckValidation(args.PropertyName);
        }

        async protected void CheckValidation(string propertyName)
        {
            if (validatorsAsync.ContainsKey(propertyName))
                await ValidateAsync(propertyName);

            else if (validators.ContainsKey(propertyName))
                Validate(propertyName);
        }

        protected void RegisterValidator(string propertyName, Func<List<string>> validatorFunc)
        {
            if (validators.ContainsKey(propertyName))
            {
                validators.Remove(propertyName);
            }
            validators[propertyName] = validatorFunc;
        }

        protected void RegisterValidatorAsync(string propertyName, Func<Task<List<string>>> validatorFunc)
        {
            if (validatorsAsync.ContainsKey(propertyName))
            {
                validatorsAsync.Remove(propertyName);
            }
            validatorsAsync[propertyName] = validatorFunc;
        }

        private void Validate(string propertyName)
        {
            if (String.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException();
            }

            Func<List<string>> validator;
            if (!validators.TryGetValue(propertyName, out validator))
            {
                return;
            }

            try
            {
                var errors = validator();
                if (errors != null && errors.Any())
                {
                    errorsByPropertyName[propertyName] = errors;
                }
                else if (errorsByPropertyName.ContainsKey(propertyName))
                {
                    errorsByPropertyName.Remove(propertyName);
                }
            }
            catch (Exception ex)
            {
                errorsByPropertyName[propertyName] = new List<string>(new[] { ex.Message }); ;
            }
            finally
            {
                OnErrorsChanged(propertyName);
            }
        }

        async protected Task<bool> ValidateAsync(string propertyName)
        {
            if (String.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentException();
            }

            Func<Task<List<string>>> validator;
            if (!validatorsAsync.TryGetValue(propertyName, out validator))
            {
                return false;
            }

            var validationProcessKey = Guid.NewGuid();
            lastValidationProcess[propertyName] = validationProcessKey;
            isValidating = true;
            try
            {
                var errors = await validator();
                if (lastValidationProcess.ContainsKey(propertyName) && lastValidationProcess[propertyName] == validationProcessKey)
                {
                    if (errors != null && errors.Any())
                    {
                        errorsByPropertyName[propertyName] = errors;
                    }
                    else if (errorsByPropertyName.ContainsKey(propertyName))
                    {
                        errorsByPropertyName.Remove(propertyName);
                    }
                }
            }
            catch (Exception ex)
            {
                errorsByPropertyName[propertyName] = new List<string>(new[] { ex.Message });
            }
            finally
            {
                if (lastValidationProcess.ContainsKey(propertyName) && lastValidationProcess[propertyName] == validationProcessKey)
                {
                    lastValidationProcess.Remove(propertyName);
                }

                IsValidating = lastValidationProcess.Any();
                OnErrorsChanged(propertyName);
            }
            return IsValid;
        }

        protected async Task<bool> ValidateAll()
        {
            foreach (var property in validators.Keys)
            {
                Validate(property);
            }

            foreach (var property in validatorsAsync.Keys)
            {
                await ValidateAsync(property);
            }
            return IsValid;
        }

        public bool HasErrors
        {
            get { return errorsByPropertyName.Any(); }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return errorsByPropertyName.ContainsKey(propertyName) ? errorsByPropertyName[propertyName] : null;
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            IsValid = !lastValidationProcess.Any() && !errorsByPropertyName.Any();
        }
    }
}

