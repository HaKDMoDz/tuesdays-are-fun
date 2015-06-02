using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TuesdaysAreFun.Tests.Game;
using TuesdaysAreFun.Windows;

namespace TuesdaysAreFun.Tests.Cards
{
	/// <summary>
	/// Base class for all card games. Will eventually
	/// be abstract
	/// </summary>
	public class CardGame
	{
		protected long FrameNum
		{ get; private set; }

		public CardRenderWindow Handle
		{ get; private set; }

		public MouseState Mouse
		{ get; private set; }

		public static readonly Vector2 CARD_SIZE = new Vector2(72, 96);

		// =========== TEST =========== //

		ImageGraphic cardSprite;
		Vector2 cardPos = new Vector2(60, 60);
		Vector2 clickOffset;

		// ============================ //

		public CardGame(CardRenderWindow window)
		{
			Handle = window;
		}

		public void Init()
		{
			Card QH = new Card(CardSuit.Hearts, CardRank.Queen);
			cardSprite = new ImageGraphic(QH.ImagePath());
			FrameNum = 0;
		}

		public void GetInput()
		{
			if (Mouse.IsClickingStart())
			{
				if (Mouse.Position.X > cardPos.X && Mouse.Position.X < cardPos.X + CARD_SIZE.X &&
					Mouse.Position.Y > cardPos.Y && Mouse.Position.Y < cardPos.Y + CARD_SIZE.Y)
				{
					clickOffset = Mouse.Position - cardPos;
				}
				else
				{
					clickOffset = null;
				}
			}
			else if (Mouse.IsClicking())
			{
				if (clickOffset != null)
				{
					cardPos = Mouse.Position - clickOffset;
				}
			}
		}

		public void SendCommand(params string[] cmd)
		{
			cardPos.Y += 30;
		}

		public void Update(TimeSpan deltaTime)
		{
			FrameNum++;

			//cardPos.X += deltaTime.TotalSeconds * 10.0;
		}

		public void Render()
		{
			Handle.BeginRender();

			Handle.RenderImage(cardSprite, cardPos);

			Handle.FinishRender();
		}
	}
}
