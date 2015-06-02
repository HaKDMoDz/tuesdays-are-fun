using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun.Commands
{
	/// <summary>
	/// So text can be said normally.
	/// </summary>
	[Command("say")]
	public class CmdSay : CommandBase
	{
		public override void Run(params string[] args)
		{
			string[] text = new string[args.Length - 1];
			for (int i = 1; i < args.Length; i++)
			{
				text[i - 1] = args[i];
			}

			Print("<", ConsoleColor.White);
			Print(Username, ConsoleColor.Cyan);
			Print(">: ", ConsoleColor.White);
			PrintLnF(text.JoinString(' '));
		}
	}
}
