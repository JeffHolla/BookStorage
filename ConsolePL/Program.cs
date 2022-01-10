using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a console instance and run it 
            // Just not to do in the main 
            UserConsole userConsole = new UserConsole();
            userConsole.Run();
        }
    }
}
