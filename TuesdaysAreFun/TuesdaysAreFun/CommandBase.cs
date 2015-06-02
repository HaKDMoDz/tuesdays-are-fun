using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TuesdaysAreFun
{
	public abstract class CommandBase
	{
		public static ConsoleColor Color
		{ get; protected set; }

		public static bool Bold
		{ get; protected set; }
		public static bool Italic
		{ get; protected set; }
		public static bool Underline
		{ get; protected set; }

		public static bool IsInitialized
		{ get; private set; }

		public static string Username
		{ get; protected set; }

		public CommandViewModel ViewModel
		{ get; set; }

		public void RunStart(CommandViewModel vm, params string[] args)
		{
			Initialize();

			ViewModel = vm;
			Run(args);
		}

		public static void Initialize()
		{
			if (IsInitialized)
			{
				return;
			}

			Color = ConsoleColor.White;

			Bold = false;
			Italic = false;
			Underline = false;

			Username = "einsteinsci";
		}

		public abstract void Run(params string[] args);

		public void Invoke(Action todo, params object[] args)
		{
			ViewModel.Window.Dispatcher.Invoke(todo, args);
		}

		public void Print(string text)
		{
			if (!CommandViewModel.IsLoaded)
			{
				return;
			}

			ViewModel.Window.Dispatcher.Invoke(ViewModel.PrintingFull, 
				text, Color, Bold, Italic, Underline);
		}
		public void Print(string text, ConsoleColor color)
		{
			Color = color;
			Print(text);
		}
		public void PrintF(string formatted)
		{
			if (!CommandViewModel.IsLoaded)
			{
				return;
			}

			ViewModel.Window.Dispatcher.Invoke(ViewModel.PrintingFormatted, formatted);
		}
		public void PrintLn(string text, ConsoleColor color)
		{
			Color = color;
			PrintLn(text);
		}
		public void PrintLn(string text)
		{
			Print(text + "\r");
		}

		public void PrintErr(string message)
		{
			if (!CommandViewModel.IsLoaded)
			{
				return;
			}

			ViewModel.Window.Dispatcher.Invoke(ViewModel.PrintingErr, message);
		}
		public void PrintLn(TextFormat text)
		{
			if (!CommandViewModel.IsLoaded)
			{
				return;
			}

			text.Text += "\r";

			ViewModel.Window.Dispatcher.Invoke(ViewModel.PrintingCustom, text);
		}
		public void PrintLnF(string formatted)
		{
			PrintF(formatted + "\r");
		}

		public void Shell(params string[] args)
		{
			if (!ViewModel.CommandExists(args[0]))
			{
				PrintErr("Command " + args[0] + " does not exist.");
				return;
			}

			string[] otherArgs = null;
			Array.Copy(args, otherArgs, 1);

			ViewModel.StartCommand(args[0], otherArgs);
		}
	}
}
