using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    class UserHelper
    {
        public static string GetText()
        {
            return Console.ReadLine();
        }
		public static string GetPassword()
		{
			string pass = "";
			ConsoleKeyInfo key;

			do
			{
				key = Console.ReadKey(true);

				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					pass += key.KeyChar;
					Console.Write("*");
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
					{
						pass = pass.Substring(0, (pass.Length - 1));
						Console.Write("\b \b");
					}
				}
			}
			// Stops Receving Keys Once Enter is Pressed
			while (key.Key != ConsoleKey.Enter);

			return pass;
		}

		//  read decimals that users input
		public static decimal GetDecimal()
		{
			string input = "";
			bool decimalStarted = false;
			ConsoleKeyInfo key;

			do
			{
				key = Console.ReadKey(true);

				if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
				{
					// accept only numbers
					if (Decimal.TryParse(key.KeyChar + "", out decimal decVal))
					{
						input += key.KeyChar;
						Console.Write(key.KeyChar);
					}
					// check for decimal 
					if (key.KeyChar == '.' && !decimalStarted)
					{
						input += key.KeyChar;
						Console.Write(key.KeyChar);
						decimalStarted = true;
					}
				}
				else
				{
					if (key.Key == ConsoleKey.Backspace && input.Length > 0)
					{
						input = input.Substring(0, (input.Length - 1));
						Console.Write("\b \b");
					}
				}
			}
			// stop reading keys if pressed enter
			while (key.Key != ConsoleKey.Enter);
			Console.WriteLine();

			// return final output value
			if (Decimal.TryParse(input, out decimal decimalVal))
			{
				return decimalVal;
			}
			return 0;
		}
	}
}
