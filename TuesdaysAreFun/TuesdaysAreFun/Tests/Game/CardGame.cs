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
	public class CardGame : IRenderer
	{
		protected long FrameNum
		{ get; private set; }

		public CardRenderWindow Handle
		{ get; protected set; }

		public MouseState Mouse
		{ get; set; }

		[Obsolete("Malfunctioning. Does not seem to receive input.")]
		public KeyboardState Keyboard
		{ get; set; }

		public static readonly Vector2 CARD_SIZE = new Vector2(72, 96);

		// =========== TEST =========== //

		GCard card = new GCard(new Card(CardSuit.Hearts, CardRank.Queen), new Vector2(30, 50));
		Vector2 clickOffset = null;

		GCardStack stack = new GCardStack(CardPile.FullDeck, new Vector2(170, 30));

		// ============================ //

		public CardGame(CardRenderWindow window)
		{
			Handle = window;
		}

		public virtual void Init()
		{
			FrameNum = 0;
		}

		public virtual void GetInput()
		{
			if (Mouse.IsClickingStart())
			{
				clickOffset = card.ClickedOffset(Mouse.Position);
			}
			else if (Mouse.IsClicking())
			{
				if (clickOffset != null)
				{
					card.Position = Mouse.Position - clickOffset;
				}
			}

			if (Keyboard.AreKeysDown(Enum.GetValues(typeof(Key))))
			{
				card.Position.Y -= 30;
			}
		}

		public virtual void SendCommand(params string[] cmd)
		{
			card.Position.Y += 30;
		}

		public virtual void Update(TimeSpan deltaTime)
		{
			FrameNum++;

			//cardPos.X += deltaTime.TotalSeconds * 10.0;
		}

		public virtual void Render()
		{
			Handle.BeginRender();

			stack.Render(Handle);
			card.Render(Handle);

			Handle.FinishRender();
		}
	}
}
