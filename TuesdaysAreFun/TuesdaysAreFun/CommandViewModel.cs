using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using TuesdaysAreFun.Commands;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun
{
	public delegate void PrintFuncFull(string text, ConsoleColor color,
								   bool bold, bool italic, bool underline);
	public delegate void PrintFunc(string text);
	public delegate void PrintFuncErr(string text);
	public delegate void PrintFuncCustom(TextFormat text);

	public sealed class CommandViewModel
	{
		Thread calculationThread;
		Thread cardWindowThread;

		public static bool IsLoaded
		{ get; set; }

		public bool DoingStuff
		{
			get
			{
				return calculationThread.ThreadState == ThreadState.Running;
			}
		}

		public PrintFuncFull PrintingFull
		{ get; private set; }
		public PrintFunc Printing
		{ get; private set; }
		public PrintFunc PrintingFormatted
		{ get; private set; }
		public PrintFuncCustom PrintingCustom
		{ get; private set; }

		public PrintFuncErr PrintingErr
		{ get; private set; }

		public MainWindow Window
		{ get; private set; }

		public RichTextBox OutputRtb
		{
			get
			{
				return Window.OutputBox;
			}
		}

		public List<string> RecentCommands
		{ get; private set; }

		public static Dictionary<string, CommandBase> Commands
		{ get; private set; }

		public CommandInputable HeldInput
		{ get; set; }

		static CommandViewModel()
		{
			IsLoaded = false;
		}

		public CommandViewModel(MainWindow win)
		{
			if (Commands == null)
			{
				Commands = new Dictionary<string, CommandBase>();
			}

			RecentCommands = new List<string>();

			PrintingFull = Print;
			PrintingErr = PrintErr;
			Printing = PrintSimple;
			PrintingCustom = PrintCustom;
			PrintingFormatted = PrintFormatted;
			Window = win;

			LoadCommands();
		}

		public void Print(string text, ConsoleColor color, bool bold, bool italic, bool underline)
		{
			TextFormat tf = new TextFormat(text, color, bold, italic, underline);
			OutputRtb.AppendText(tf);

			OutputRtb.ScrollToEnd();
		}
		public void PrintSimple(string text)
		{
			Print(text, ConsoleColor.White, false, false, false);
		}
		public void PrintErr(string message)
		{
			Print(message + "\r", ConsoleColor.DarkRed, true, false, false);
		}
		public void PrintCustom(TextFormat text)
		{
			OutputRtb.AppendText(text);
			OutputRtb.ScrollToEnd();
		}
		public void PrintFormatted(string formatted)
		{
			OutputRtb.AppendTextF(formatted);
			OutputRtb.ScrollToEnd();
		}

		public void LoadCommands()
		{
			Type[] commands = Util.GetTypesWithAttribute<CommandAttribute>(Assembly.GetExecutingAssembly());

			foreach (Type cmd in commands)
			{
				AddCommand(cmd);
			}
		}
		public void AddCommand(Type underlyingType)
		{
			if (!typeof(CommandBase).IsAssignableFrom(underlyingType))
			{
				throw new ArgumentException("Command must inherit from CommandBase.");
			}

			CommandBase cmd = Activator.CreateInstance(underlyingType) as CommandBase;
			CommandAttribute att = Attribute.GetCustomAttribute(underlyingType, 
				typeof(CommandAttribute)) as CommandAttribute;
			
			if (Commands.ContainsKey(att.CommandName))
			{
				throw new InvalidOperationException("Comand is already registered to that name.");
			}

			Commands.Add(att.CommandName, cmd);
		}

		public void HandleInput(string cmd)
		{
			if (calculationThread != null)
			{
				if (DoingStuff)
				{
					Print("30 seconds to finish previous task.\r", ConsoleColor.Red,
						false, true, false);
				}

				calculationThread.Join(TimeSpan.FromSeconds(30));
			}

			RecentCommands.Add(cmd);

			if (HeldInput != null)
			{
				calculationThread = new Thread(HeldInput.HandleInput);
				calculationThread.Start(cmd);
				return;
			}

			calculationThread = new Thread(RunCommand);
			calculationThread.SetApartmentState(ApartmentState.STA);
			calculationThread.Name = "Command Thread";
			calculationThread.Start(cmd);
		}

		private void RunCommand(object cmdObj)
		{
			CommandBase.Initialize();

			string cmd = cmdObj as string;
			if (cmd == null)
			{
				Window.Dispatcher.Invoke(PrintingErr, "Command input is not of type System.String");
				return;
			}

			if (cmd.Trim() == "")
			{
				return;
			}

			if (!cmd.StartsWithParams("/", "-", "#"))
			{
				StartCommand("say", "say", cmd);
				return;
			}

			cmd = cmd.TrimStart('/', '-', '#');

			string[] args = cmd.Split(' ');
			string name = args[0];

			if (!Commands.ContainsKey(name))
			{
				OutputRtb.Dispatcher.Invoke(PrintingErr, "Command not recognized.");
				return;
			}

			StartCommand(name, args);
		}

		public void StartCommand(string name, params string[] args)
		{
			Commands[name].RunStart(this, args);
		}

		public bool CommandExists(string name)
		{
			return Commands.ContainsKey(name);
		}

		public CardRenderWindow LaunchCardWindow(CmdCards cmd)
		{
			CardRenderWindow res = null;

			cardWindowThread = new Thread(() =>
			{
				CardRenderWindow crw = new CardRenderWindow(cmd, this);
				res = crw;
				crw.Closed += (sender, e) =>
				{
					crw.Dispatcher.InvokeShutdown();
					cmd.ReleaseInput();
				};

				crw.Show();
				System.Windows.Threading.Dispatcher.Run();
			});

			cardWindowThread.SetApartmentState(ApartmentState.STA);
			cardWindowThread.Start();

			Window.Dispatcher.Invoke(PrintingFormatted, "&e&*Starting Window...\r");

			while (res == null) // sorta hacky workaround
			{
				Thread.Sleep(1); // prevent excessive memory use
			}

			return res;
		}
	}
}
