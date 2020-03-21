using System;

namespace CSharpProgramming2020.Task2.Exceptions
{
    internal class InvalidEmailException:Exception
    {
        private string _message;

        internal InvalidEmailException(string message)
        {
            _message = message;
        }

        public override string Message
        {
            get => _message;
        }
    }
}
