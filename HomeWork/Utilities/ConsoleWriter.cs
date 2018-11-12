using System;

namespace HomeWork.Client.Utilities
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}