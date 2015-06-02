using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun.Commands
{
	[Command("repeat")]
	public sealed class CmdRepeat : CommandInputable
	{
		string repeated;

		public override void ReroutedInput(string cmd)
		{
			string res = cmd.Trim();
			if (res.ToLower() == "/exit")
			{
				PrintLn("End.", ConsoleColor.Yellow);

				ReleaseInput();
			}
			else if (res == repeated)
			{
				PrintLn("Correct.", ConsoleColor.Green);
				Redo();
				PrintLn("New word: " + repeated, ConsoleColor.Blue);
			}
			else
			{
				PrintLn("Incorrect. Try again. Type '/exit' to return to main console.", 
					ConsoleColor.Red);
			}
		}

		public override void Run(params string[] args)
		{
			Redo();

			bool hasInput = GrabInput();
			if (!hasInput)
			{
				PrintErr("Could not grab input.");
			}

			PrintLn("Repeat after me: " + repeated, ConsoleColor.Blue);
		}

		public void Redo()
		{
			repeated = "";
			for (int i = 0; i < 5; i++)
			{
				repeated += Util.RandomLetter(false);
			}
		}
	}
}
