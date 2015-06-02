using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TuesdaysAreFun.Tests.Game
{
	public class KeyboardState
	{
		public KeyboardDevice Device
		{ get; private set; }

		public KeyboardState(KeyboardDevice device)
		{
			Device = device;
		}

		public void Update()
		{

		}
	}
}
