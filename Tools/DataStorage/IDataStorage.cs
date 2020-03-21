using System.Collections.Generic;
using CSharpProgramming2020.Task2.Models;

namespace CSharpProgramming2020.Task2.Tools.DataStorage
{
    internal interface IDataStorage
    {

        void Add(Person person);

        void Remove(Person person);

        void DoChanges();

        List<Person> PersonsList { get; }
    }
}
