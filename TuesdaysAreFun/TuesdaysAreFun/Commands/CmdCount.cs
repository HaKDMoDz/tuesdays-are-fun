using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun.Commands
{
	[Command("count")]
	public class CmdCount : CommandBase
	{
		private int count = 0;

		public override void Run(params string[] args)
		{
			count++;
			PrintLn("Total: " + count.ToString());
		}
	}
}
