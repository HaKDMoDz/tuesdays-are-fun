using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun
{
	public abstract class CommandInputable : CommandBase
	{
		public abstract void ReroutedInput(string cmd);

		public void HandleInput(object cmdObj)
		{
			string cmd = cmdObj as string;
			if (cmd == null)
			{
				PrintErr("Command input is not of type System.String");
				return;
			}

			ReroutedInput(cmd);
		}

		public bool GrabInput()
		{
			if (ViewModel.HeldInput == null)
			{
				ViewModel.HeldInput = this;
				return true;
			}

			return false;
		}

		public void ReleaseInput()
		{
			ViewModel.HeldInput = null;
		}
	}
}
