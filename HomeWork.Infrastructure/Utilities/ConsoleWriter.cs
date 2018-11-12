using System;

namespace HomeWork.Infrastructure.Utilities
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}