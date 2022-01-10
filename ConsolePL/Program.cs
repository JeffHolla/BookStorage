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
            UserConsole userConsole = new UserConsole();
            userConsole.Run();
        }
    }
}
