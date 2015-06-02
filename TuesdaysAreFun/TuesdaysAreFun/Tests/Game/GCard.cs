using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuesdaysAreFun.Tests.Cards;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun.Tests.Game
{
	public class GCard : IRenderable
	{
		public Card InternalCard
		{ get; set; }

		public Vector2 Position
		{ get; set; }

		public GCard(Card datum, Vector2 position = null)
		{
			InternalCard = datum;
			Position = position ?? new Vector2(0, 0);
		}

		public Vector2 ClickedOffset(Vector2 clickLoc)
		{
			if (clickLoc.X >= Position.X && clickLoc.X <= Position.X + CardGame.CARD_SIZE.X &&
				clickLoc.Y >= Position.Y && clickLoc.Y <= Position.Y + CardGame.CARD_SIZE.Y)
			{
				return clickLoc - Position;
			}

			return null;
		}

		public void Render(CardRenderWindow handle)
		{
			IGraphic img = new ImageGraphic(InternalCard.ImagePath());
			handle.RenderImage(img, Position);
		}
	}
}
