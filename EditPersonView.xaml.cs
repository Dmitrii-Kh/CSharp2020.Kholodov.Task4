using System.Windows.Controls;
using CSharpProgramming2020.Task2.Tools.Navigation;
using CSharpProgramming2020.Task2.VIewModels;

namespace CSharpProgramming2020.Task2
{
    /// <summary>
    /// Логика взаимодействия для EditPersonView.xaml
    /// </summary>
    public partial class EditPersonView : UserControl, INavigatable
    {
        public EditPersonView()
        {
            InitializeComponent();
            DataContext = new EditPersonViewModel();
        }
    }
}
