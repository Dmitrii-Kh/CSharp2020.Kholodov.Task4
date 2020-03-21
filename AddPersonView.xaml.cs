using System.Windows.Controls;
using CSharpProgramming2020.Task2.Tools.Navigation;
using CSharpProgramming2020.Task2.VIewModels;

namespace CSharpProgramming2020.Task2
{
    /// <summary>
    /// Логика взаимодействия для AddPersonView.xaml
    /// </summary>
    public partial class AddPersonView : UserControl, INavigatable
    {
        public AddPersonView()
        {
            InitializeComponent();
            DataContext = new AddPersonViewModel();
        }
    }
}
