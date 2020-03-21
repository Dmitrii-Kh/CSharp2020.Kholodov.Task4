using System.Windows.Controls;
using CSharpProgramming2020.Task2.Tools.Managers;
using CSharpProgramming2020.Task2.Tools.Navigation;
using CSharpProgramming2020.Task2.VIewModels;

namespace CSharpProgramming2020.Task2
{
    /// <summary>
    /// Логика взаимодействия для TableView.xaml
    /// </summary>
    public partial class TableView : UserControl, INavigatable
    {

        public TableView()
        {
            InitializeComponent();
            DataContext = new TableViewModel();
            StationManager.PersonTable = TableGrid;
        }

    }
}
