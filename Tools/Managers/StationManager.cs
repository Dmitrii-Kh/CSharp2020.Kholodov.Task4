using System;
using System.Windows;
using System.Windows.Controls;
using CSharpProgramming2020.Task2.Models;
using CSharpProgramming2020.Task2.Tools.DataStorage;
using CSharpProgramming2020.Task2.VIewModels;

namespace CSharpProgramming2020.Task2.Tools.Managers
{
    internal static class StationManager
    {
        internal static Person CurrentPerson { get; set; }
        internal static Person PersonToEdit { get; set; }
        internal static DataGrid PersonTable { get; set; }
        internal static EditPersonViewModel EditPersonVM { get; set; }
        internal static TableViewModel TablePersonVM { get; set; }

        private static IDataStorage _dataStorage;

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("Shutting Down");
            Environment.Exit(1);
        }

    }
}
