using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun.Tests.Game
{
	interface IRenderable
	{
		void Render(CardRenderWindow handle);
	}
}
