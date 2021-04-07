using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    public abstract class Base
    {
        public void ShowError()
        {
            Console.WriteLine("Invalid option. Please select valid option\n");
            Execute(false);
        }
        public abstract void Execute(bool clearScreen = true);
    }
}
