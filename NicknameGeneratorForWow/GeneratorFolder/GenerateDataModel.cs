using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace NicknameGeneratorForWow
{
    public class GenerateData : INotifyPropertyChanged, IDataErrorInfo
    {
        private char firstLetter;
        private int minLength;
        private int maxLength;

        public GenerateData()
        {
            firstLetter = 'A';
            minLength = 5;
            maxLength = 10;

            errors = new Dictionary<string, string>();
        }
        public char FirstLetter
        {
            get { return firstLetter; }
            set
            {
                firstLetter = char.ToUpper(value);
                Regex check = new Regex(@"[A-Z]");
                if (!check.IsMatch(firstLetter.ToString()))
                {
                    errors["FirstLetter"] = "must be latin letter";
                }
                else
                {
                    errors["FirstLetter"] = null;
                }
                OnPropertyChanged("FirstLetter");
            }
        }
        public int MinLength
        {
            get { return minLength; }
            set
            {
                minLength = value;
               
                if (minLength < 5)
                {
                    errors["MinLength"] = "Minimum 5";
                }
                else if (minLength > maxLength)
                {
                    errors["MinLength"] = "Min can't be more max";
                }
                else
                {
                    errors["MinLength"] = null;
                }
                OnPropertyChanged("MinLength");
            }
        }
        public int MaxLength
        {
            get { return maxLength; }
            set
            {
                maxLength = value;
                if (maxLength > 10)
                {
                    errors["MaxLength"] = "Maximum 10";
                }
                else if (minLength > maxLength)
                {
                    errors["MaxLength"] = "Min can't be more max";
                }
                else
                {
                    errors["MaxLength"] = null;
                }
                OnPropertyChanged("MaxLength");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        // Валидация 
        Dictionary<string, string> errors;
        public string this[string columnName] => errors.ContainsKey(columnName) ? errors[columnName] : null;
        
        // Если все тексты ошибок null - данные валидные
        public bool IsValid => !errors.Values.Any(x => x != null);
        public string Error
        {
            get
            {
                return null;
            }
        }
    }
}