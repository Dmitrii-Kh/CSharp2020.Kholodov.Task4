using System.Windows;
using System.Windows.Controls;
using CSharpProgramming2020.Task2.Tools.DataStorage;
using CSharpProgramming2020.Task2.Tools.Managers;
using CSharpProgramming2020.Task2.Tools.Navigation;
using CSharpProgramming2020.Task2.VIewModels;


namespace CSharpProgramming2020.Task2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }


        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.TableView);

        }
    }
}
