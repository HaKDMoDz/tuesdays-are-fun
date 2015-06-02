using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun.Tests.Game
{
	public class Sprite
	{
		public IGraphic Image
		{ get; private set; }

		public Vector2 Position
		{ get; private set; }

		public Sprite(string path, Vector2 startPos = null)
		{

		}

		public void Render(CardRenderWindow handle)
		{

		}
	}
}
