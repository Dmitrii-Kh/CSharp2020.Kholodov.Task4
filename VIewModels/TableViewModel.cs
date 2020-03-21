using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpProgramming2020.Task2.Models;
using CSharpProgramming2020.Task2.Tools;
using CSharpProgramming2020.Task2.Tools.Managers;
using CSharpProgramming2020.Task2.Tools.Navigation;

namespace CSharpProgramming2020.Task2.VIewModels
{
    internal class TableViewModel : BaseViewModel
    {
        public TableViewModel()
        {
            StationManager.TablePersonVM = this;
        }

        #region Fields

        private List<Person> _personList = StationManager.DataStorage.PersonsList;
        private string[] _sortCases =
        {
            "Name",
            "Surname",
            "Email",
            "Date of Birth",
            "Sun Sign",
            "Chinese Sign",
            "Adult",
            "BDay Today"
        };

        private string[] _searchCases =
        {
            "Name",
            "Surname",
            "Email",
            "Date of Birth",
            "Sun Sign",
            "Chinese Sign",
            "Adult",
            "BDay Today"
        };

        private string[] _sortStrategies =
        {
            "Ascending",
            "Descending"
        };

        private RelayCommand<object> _addPersonCommand;
        private RelayCommand<object> _editPersonCommand;
        private RelayCommand<object> _removePersonCommand;
        private RelayCommand<object> _searchCommand;

        private int _sortCase = 0;
        private int _searchCase = 0;
        private int _sortStrategy = 0; 

        #endregion

        #region Properties

        public object SelectedPerson { get; set; }
        public string SearchQuery { get; set; }

        public int SortCase
        {
            get { return _sortCase; }
            set
            {
                _sortCase = value;
                OnPropertyChanged("PersonList");
            }
        }

        public int SearchCase
        {
            get { return _searchCase; }
            set
            {
                _searchCase = value;
                OnPropertyChanged("PersonList");
            }
        }

        public int SortStrategy 
        {
            get { return _sortStrategy; }
            set
            {
                _sortStrategy = value;
                OnPropertyChanged("PersonList");
            }
        }

        public IEnumerable<Person> PersonList
        {
            get
            {
                IEnumerable<Person> peopleList = _personList;

                if (SortStrategy == 0)
                {
                    switch (SortCase)
                    {
                        case 0:
                            peopleList = peopleList.OrderBy(x => x.Name);
                            break;
                        case 1:
                            peopleList = peopleList.OrderBy(x => x.Surname);
                            break;
                        case 2:
                            peopleList = peopleList.OrderBy(x => x.Email);
                            break;
                        case 3:
                            peopleList = peopleList.OrderBy(x => x.Birthday);
                            break;
                        case 4:
                            peopleList = peopleList.OrderBy(x => x.SunSign);
                            break;
                        case 5:
                            peopleList = peopleList.OrderBy(x => x.ChineseSign);
                            break;
                        case 6:
                            peopleList = peopleList.OrderBy(x => x.IsAdult);
                            break;
                        case 7:
                            peopleList = peopleList.OrderBy(x => x.IsBirthday);
                            break;
                    }
                }
                else
                {
                    switch (SortCase)
                    {
                        case 0:
                            peopleList = peopleList.OrderByDescending(x => x.Name);
                            break;
                        case 1:
                            peopleList = peopleList.OrderByDescending(x => x.Surname);
                            break;
                        case 2:
                            peopleList = peopleList.OrderByDescending(x => x.Email);
                            break;
                        case 3:
                            peopleList = peopleList.OrderByDescending(x => x.Birthday);
                            break;
                        case 4:
                            peopleList = peopleList.OrderByDescending(x => x.SunSign);
                            break;
                        case 5:
                            peopleList = peopleList.OrderByDescending(x => x.ChineseSign);
                            break;
                        case 6:
                            peopleList = peopleList.OrderByDescending(x => x.IsAdult); 
                            break;
                        case 7:
                            peopleList = peopleList.OrderByDescending(x => x.IsBirthday);
                            break;
                    }
                }

                if (String.IsNullOrWhiteSpace(SearchQuery))
                    return peopleList;

                switch (SearchCase)
                {
                    case 0:
                        peopleList = peopleList.Where(x => x.Name.Contains(SearchQuery));
                        break;
                    case 1:
                        peopleList = peopleList.Where(x => x.Surname.Contains(SearchQuery));
                        break;
                    case 2:
                        peopleList = peopleList.Where(x => x.Email.Contains(SearchQuery));
                        break;
                    case 3:
                        peopleList = peopleList.Where(x => x.Birthday.ToString().Contains(SearchQuery));
                        break;
                    case 4:
                        peopleList = peopleList.Where(x => x.SunSign.Contains(SearchQuery));
                        break;
                    case 5:
                        peopleList = peopleList.Where(x => x.ChineseSign.Contains(SearchQuery));
                        break;
                    case 6:
                        peopleList = peopleList.Where(x => x.IsAdult);          // todo
                        break;
                    case 7:
                        peopleList = peopleList.Where(x => x.IsBirthday);
                        break;
                }

                return peopleList;
            }
        }

        public IEnumerable<string> SortCasesEnum
        {
            get { return _sortCases; }
        }

        public IEnumerable<string> SearchCasesEnum
        {
            get { return _searchCases; }
        }

        public IEnumerable<string> SortStrategies
        {
            get { return _sortStrategies; }
        }

        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPersonCommand ?? (_addPersonCommand = new RelayCommand<object>(
                           AddPersonImplementation));
            }
        }

        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _editPersonCommand ?? (_editPersonCommand =
                           new RelayCommand<object>(EditPersonImplementation, CanExecuteRemoveOrEdit));
            }
        }

        public RelayCommand<object> RemovePersonCommand
        {
            get
            {
                return _removePersonCommand ?? (_removePersonCommand =
                           new RelayCommand<object>(RemovePersonImplementation, CanExecuteRemoveOrEdit));
            }
        }

        public RelayCommand<object> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand<object>(
                           (o => { OnPropertyChanged("PersonList"); })));
            }
        }

        #endregion

        private void AddPersonImplementation(object obj)
        {
            StationManager.CurrentPerson = new Person("", "", "");
            NavigationManager.Instance.Navigate(ViewType.AddPersonView);
        }

        private async void RemovePersonImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                Person personToRemove = (Person)SelectedPerson;
                StationManager.DataStorage.Remove(personToRemove);
                OnPropertyChanged("PersonList");

            });
            LoaderManager.Instance.HideLoader();
        }

        private async void EditPersonImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            await Task.Run(() =>
            {
                StationManager.CurrentPerson = (Person)SelectedPerson;

                StationManager.PersonToEdit = new Person(
                    StationManager.CurrentPerson.Name,
                    StationManager.CurrentPerson.Surname,
                    StationManager.CurrentPerson.Email,
                    StationManager.CurrentPerson.Birthday
                );
            });
            LoaderManager.Instance.HideLoader();
            if (StationManager.EditPersonVM != null)
                StationManager.EditPersonVM.Update();

            NavigationManager.Instance.Navigate(ViewType.EditPersonView);
        }

        private bool CanExecuteRemoveOrEdit(object obj)
        {
            return SelectedPerson != null;
        }

        public void UpdateInfo()
        {
            OnPropertyChanged("PersonList");
        }
    }
}
