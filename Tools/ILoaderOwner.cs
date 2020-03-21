using System.Windows;

namespace CSharpProgramming2020.Task2.Tools
{
    internal interface ILoaderOwner 
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}
