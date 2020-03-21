namespace CSharpProgramming2020.Task2.Tools.Navigation
{
    internal enum ViewType
    {
        TableView,
        AddPersonView,
        EditPersonView
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
