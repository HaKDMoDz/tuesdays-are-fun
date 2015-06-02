using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun.Commands
{
	[Command("echo")]
	public class CmdEcho : CommandBase
	{
		public override void Run(params string[] args)
		{
			if (args.Length <= 1)
			{
				return;
			}

			string text = Util.AllButFirstCompiled(args);

			PrintLn(text, ConsoleColor.Gray);
		}
	}
}
