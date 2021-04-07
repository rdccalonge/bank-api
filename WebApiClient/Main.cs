using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    class Main : Base
    {
        public override void Execute(bool clearScreen = true)
        {
            
            Console.Clear();
            Console.WriteLine("ERNI BANK SYSTEM!\n");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("\nPlease enter your choice: ");

           
            if (Int32.TryParse(Console.ReadLine(), out int result))
			{
                

                switch (result)
				{
                    case 1:
                        {
                            new RegisterForm().Execute();
                            break;
                        }
                    case 2:
                        {
                            new LoginForm().Execute();
                            break;
                        }
                    case 3:
                        {
                            Environment.Exit(0);
                            break;
                        }
                    default:
                        {
                            ShowError();
                            break;
                        }
                }
			}
			else
			{
				ShowError();
			}
		}
		


    }
}
