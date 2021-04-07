using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
	public class LoginForm : Base
	{
		public override void Execute(bool clearScreen = true)
		{
			Console.Clear();
			Console.WriteLine("1. Login User");
			Console.WriteLine("2. Go to Main Screen\n");
			Console.Write("\nPlease enter your choice: ");
			if (Int32.TryParse(Console.ReadLine(), out int input))
			{
				switch (input)
				{
					case 1:
						{
							Login();
							break;
						}
					case 2:
						{
							new Main().Execute();
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

		// function that authenticates user
		public void Login()
		{
			Console.Write("Enter your username: ");
			var username = UserHelper.GetText();

			Console.Write("Enter your password: ");
			var pass = UserHelper.GetPassword();
			Console.WriteLine();

			Console.Write("Re-enter your password: ");
			var confirmpass = UserHelper.GetPassword();
			Console.WriteLine();

			//if (pass != confirmpass)
			//{
			//	Console.WriteLine("Passwords not matched.");
			//	Register();
			//	return;
			//}

			//var user = new User
			//{
			//	Username = username,
			//	Password = pass
			//};

			//var res = new AuthService().Register(user);
			//if (res.Success)
			//{
			//	Console.WriteLine("Registered : " + username);

			//	new LoginScreen().Run(false);
			//	return;
			//}
			//else
			//{
			//	Console.WriteLine("Error : " + res.Errors);
			//	Register();
			//	return;
			//}
		}
	}
}
