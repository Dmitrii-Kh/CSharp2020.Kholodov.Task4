using System;
using System.Threading.Tasks;
using System.Windows;
using CSharpProgramming2020.Task2.Exceptions;
using CSharpProgramming2020.Task2.Models;
using CSharpProgramming2020.Task2.Tools;
using CSharpProgramming2020.Task2.Tools.Managers;
using CSharpProgramming2020.Task2.Tools.Navigation;

namespace CSharpProgramming2020.Task2.VIewModels
{
    class AddPersonViewModel : BaseViewModel
    {
        #region Fields

        private Person _person = StationManager.CurrentPerson;

        private RelayCommand<object> _proceedCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion

        public AddPersonViewModel()
        {
        }

        #region Properties
        public Person PersonToAdd

        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand<Object> ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand<object>(
                           ProceedImplementation, CanExecuteProceed));
            }
        }

        public RelayCommand<Object> CancelCommand
        {
            get
            {
                return _cancelCommand ?? (_cancelCommand = new RelayCommand<object>(
                           CancelImplementation));
            }
        }

        #endregion

        private async void ProceedImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();

            bool res = await Task.Run(() =>
            {
                try
                {
                    _person.Validate();
                }
                catch (BirthdayInFutureException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                catch (BirthdayInDistantPastException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                catch (InvalidEmailException e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }
                return true;
            });

            LoaderManager.Instance.HideLoader();
            if (res)
            {
                StationManager.DataStorage.Add(_person);
                _person = new Person("", "", "");
                PersonToAdd = _person;
                StationManager.TablePersonVM.UpdateInfo();
                NavigationManager.Instance.Navigate(ViewType.TableView);
            }
        }

        private bool CanExecuteProceed(Object obj)
        {
            return !String.IsNullOrWhiteSpace(PersonToAdd.Email) &&
                   !String.IsNullOrWhiteSpace(PersonToAdd.Name) &&
                   !String.IsNullOrWhiteSpace(PersonToAdd.Surname) &&
                   PersonToAdd.DateOfBirthNullable != null;
        }

        private void CancelImplementation(object obj)
        {
            StationManager.TablePersonVM.UpdateInfo();
            NavigationManager.Instance.Navigate(ViewType.TableView);
        }
    }
}
