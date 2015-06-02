using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuesdaysAreFun.Tests.Cards;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun.Tests.Game
{
	// Face-down card stack.
	public sealed class GCardStack : IRenderable
	{
		public const int CARDS_PER_IMG = 8;
		public const int OFFSET_PER_CARD = 2; // px
		public const string BACK_FILENAME = "assets/b1fv.png";

		public bool CanFlipTop
		{ get; set; }

		public CardPile InnerPile
		{ get; set; }

		public Vector2 Position
		{ get; set; }

		public GCardStack(CardPile initialPile, Vector2 position = null, bool flipTop = false)
		{
			InnerPile = initialPile;
			CanFlipTop = flipTop;
			Position = position ?? new Vector2(0, 0);
		}

		public GCardStack() : this(new CardPile())
		{ }

		public void Render(CardRenderWindow handle)
		{
			List<Vector2> cardPos = new List<Vector2>();
			Vector2 current = Position.Clone() as Vector2;
			for (int i = 0; i < InnerPile.Count; i += CARDS_PER_IMG)
			{
				cardPos.Add(current);

				current.X += OFFSET_PER_CARD;
				current.Y += OFFSET_PER_CARD;
			}

			for (int i = cardPos.Count - 1; i >= 0; i--)
			{
				IGraphic back = new ImageGraphic(BACK_FILENAME);
				handle.RenderImage(back, cardPos[i]);
			}
		}
	}
}
