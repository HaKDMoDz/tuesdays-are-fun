using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun.Tests.Cards
{
	public class CardPile : Stack<Card>
	{
		public static CardPile FullDeck
		{
			get
			{
				return _fullDeck;
			}
		}
		static CardPile _fullDeck;

		public CardPile() : base()
		{ }

		public CardPile(IEnumerable<Card> collection) : base(collection)
		{ }

		public CardPile(params Card[] cards) : base(cards)
		{ }

		static CardPile()
		{
			_fullDeck = new CardPile();
			for (CardSuit s = CardSuit.Clubs; s <= CardSuit.Spades; s++)
			{
				for (CardRank r = CardRank.Ace; r <= CardRank.King; r++)
				{
					_fullDeck.Push(new Card(s, r));
				}
			}
		}

		public bool Contains(CardRank rank)
		{
			foreach (Card c in this)
			{
				if (c.Rank == rank)
				{
					return true;
				}
			}
			return false;
		}

		public CardPile DrawGroup(int count)
		{
			CardPile pile = new CardPile();
			for (int i = 0; i < count; i++)
			{
				pile.Push(this.Pop()); 
			}
			return pile;
		}

		public CardPile Shuffle()
		{
			List<Card> list = this.ToList();
			CardPile result = new CardPile();

			Random rand = new Random();

			while (list.Count > 0)
			{
				Card sacrifice = list[rand.Next(list.Count)];
				result.Push(sacrifice);
				list.Remove(sacrifice);
			}

			return result;
		}

		public void DealTo(CardPile pile, int count = 1)
		{
			for (int i = 0; i < count; i++)
			{	
				if (Count == 0)
				{
					return;
				}

				Card moved = Pop();
				pile.Push(moved);
			}
		}

		public void DealToAll(IEnumerable<CardPile> piles, int count = 1)
		{
			for (int i = 0; i < count; i++)
			{
				foreach (CardPile p in piles)
				{
					if (Count == 0)
					{
						return;
					}

					Card moved = Pop();
					p.Push(moved);
				}
			}
		}

		public CardPile Invert()
		{
			List<Card> list = this.ToList();
			CardPile res = new CardPile();

			for (int i = list.Count; i > 0; i--)
			{
				res.Push(list[i]);
			}

			return res;
		}
	}
}
