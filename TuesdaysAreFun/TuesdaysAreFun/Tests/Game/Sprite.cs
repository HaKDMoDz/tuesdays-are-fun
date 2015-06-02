using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuesdaysAreFun.Windows;
using System.Windows.Controls;

namespace TuesdaysAreFun.Tests.Game
{
	public class Sprite : IRenderable
	{
		public IGraphic Image
		{ get; private set; }

		public Vector2 Position
		{ get; private set; }

		public Sprite(string path, Vector2 startPos = null)
		{
			Image = new ImageGraphic(path);
			if (startPos != null)
			{
				Position = startPos;
			}
			else
			{
				Position = new Vector2(0, 0);
			}
		}

		public void Render(CardRenderWindow handle)
		{
			handle.RenderImage(Image, Position);
		}
	}
}
