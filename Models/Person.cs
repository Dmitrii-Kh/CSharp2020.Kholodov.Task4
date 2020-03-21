using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using CSharpProgramming2020.Task2.Exceptions;

namespace CSharpProgramming2020.Task2.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _dateOfBirth;
        private string _sunSign;
        private string _chineseSign;
        private bool? _isAdult;
        private bool? _isBirthday;
        private DateTime? _dateOfBirthNullable;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public bool IsAdult
        {
            get { return (bool)(_isAdult ?? (_isAdult = IsAdultFunc())); }
        }

        public string SunSign
        {
            get { return _sunSign ?? (_sunSign = CalcSunSign()); }
        }

        public string ChineseSign
        {
            get { return _chineseSign ?? (_chineseSign = CalcChSign()); }
        }

        public bool IsBirthday
        {
            get { return (bool)(_isBirthday ?? (_isBirthday = IsBirthdayFunc())); }
        }

        public DateTime Birthday
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                _sunSign = CalcSunSign();
                _chineseSign = CalcChSign();
                _isAdult = IsAdultFunc();
                _isBirthday = IsBirthdayFunc();

                OnPropertyChanged();
                OnPropertyChanged("IsAdult");
                OnPropertyChanged("IsBirthday");
                OnPropertyChanged("SunSign");
                OnPropertyChanged("ChineseSign");
            }
        }

        public DateTime? DateOfBirthNullable
        {
            get { return _dateOfBirthNullable; }
            set
            {
                _dateOfBirthNullable = value;
                try
                {
                    _dateOfBirth = (DateTime)value;
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Please choose your birth date!");
                }
            }
        }

        public string BirthdayToString => _dateOfBirth.ToString("dd MMMM yyyy");

        #endregion

        #region Constructors
        internal Person(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            _name = firstName;
            _surname = lastName;
            _email = email;
            _dateOfBirth = dateOfBirth;
        }

        internal Person(string firstName, string lastName, string email) : this(firstName, lastName, email, new DateTime(1980, 01, 01))
        { }

        internal Person(string firstName, string lastName, DateTime dateOfBirth) : this(firstName, lastName, null, dateOfBirth)
        { }

        #endregion

        #region Validations
        string _pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                          @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";         //msdn
        internal void ValidateBirthday()
        {
            if (Birthday > DateTime.Today)
            {
                throw new BirthdayInFutureException("You were born in future! You cannot register unborn people!");
            }
            if (DateTime.Today.Year - Birthday.Year > 135)
            {
                throw new BirthdayInDistantPastException("The age is > 135. You cannot register dead men!");
            }
        }

        internal void ValidateEmail()
        {
            if (!Regex.IsMatch(Email, _pattern, RegexOptions.IgnoreCase))
                throw new InvalidEmailException("Invalid E-mail format!");
        }

        internal void Validate()
        {
            ValidateBirthday();
            ValidateEmail();
        }
        #endregion

        #region AdditionalFuncs
        internal bool IsAdultFunc()
        {
            return (((DateTime.Today - Birthday).Days / 365) >= 18);
        }

        internal bool IsBirthdayFunc()
        {
            return (_dateOfBirth.Day == DateTime.Today.Day) && (_dateOfBirth.Month == DateTime.Today.Month);
        }

        internal string CalcSunSign()
        {
            string[] _westernSigns = { "Aquarius", "Pisces", "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Saggitarius", "Capricorn" };
            int day = _dateOfBirth.Day;
            int month = _dateOfBirth.Month;
            string _sunSign = "";

            if (month == 1 || month == 4)
                _sunSign = day >= 20 ? _westernSigns[month - 1] : (month == 1 ? _westernSigns[_westernSigns.Length - 1] : _westernSigns[month - 2]);

            if (month == 2)
                _sunSign = day >= 19 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            if (month == 3 || month == 5 || month == 6)
                _sunSign = day >= 22 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            if (month == 7 || month == 8 || month == 9 || month == 10)
                _sunSign = day >= 23 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            if (month == 11 || month == 12)
                _sunSign = day >= 22 ? _westernSigns[month - 1] : _westernSigns[month - 2];

            return _sunSign;
        }

        internal string CalcChSign()
        {
            string[] _chineseSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            return _chineseSigns[(_dateOfBirth.Year - 4) % 12];
        }
        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
