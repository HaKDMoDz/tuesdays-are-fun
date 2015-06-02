using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuesdaysAreFun.Tests.Cards
{
	public enum CardSuit
	{
		[CustomString("C")]
		Clubs,
		[CustomString("D")]
		Diamonds,
		[CustomString("H")]
		Hearts,
		[CustomString("S")]
		Spades
	}

	public enum CardRank
	{
		[CustomString(" A")]
		Ace = 1,
		[CustomString(" 2")]
		Two = 2,
		[CustomString(" 3")]
		Three = 3,
		[CustomString(" 4")]
		Four = 4,
		[CustomString(" 5")]
		Five = 5,
		[CustomString(" 6")]
		Six = 6,
		[CustomString(" 7")]
		Seven = 7,
		[CustomString(" 8")]
		Eight = 8,
		[CustomString(" 9")]
		Nine = 9,
		[CustomString("10")]
		Ten = 10,
		[CustomString(" J")]
		Jack = 11,
		[CustomString(" Q")]
		Queen = 12,
		[CustomString(" K")]
		King = 13
	}
	
	public struct Card
	{
		public CardSuit Suit
		{ get; private set; }

		public CardRank Rank
		{ get; private set; }

		public Card(CardSuit suit, CardRank rank) : this()
		{
			Suit = suit;
			Rank = rank;
		}

		public static bool operator==(Card a, Card b)
		{
			return a.Equals(b);
		}
		public static bool operator!=(Card a, Card b)
		{
			return !a.Equals(b);
		}

		public override bool Equals(object obj)
		{
			try
			{
				Card c = (Card)obj;

				return Suit == c.Suit && Rank == c.Rank;
			}
			catch (InvalidCastException)
			{
				return false;
			}
		}

		public int ImageNum()
		{
			int suit = -1;
			switch (Suit)
			{
			case CardSuit.Clubs:
				suit = 0;
				break;
			case CardSuit.Diamonds:
				suit = 3;
				break;
			case CardSuit.Hearts:
				suit = 2;
				break;
			case CardSuit.Spades:
				suit = 1;
				break;
			default:
				break;
			}

			int rank = -1;
			switch (Rank)
			{
			case CardRank.Ace:
				rank = 0;
				break;
			case CardRank.Two:
				rank = 12;
				break;
			case CardRank.Three:
				rank = 11;
				break;
			case CardRank.Four:
				rank = 10;
				break;
			case CardRank.Five:
				rank = 9;
				break;
			case CardRank.Six:
				rank = 8;
				break;
			case CardRank.Seven:
				rank = 7;
				break;
			case CardRank.Eight:
				rank = 6;
				break;
			case CardRank.Nine:
				rank = 5;
				break;
			case CardRank.Ten:
				rank = 4;
				break;
			case CardRank.Jack:
				rank = 3;
				break;
			case CardRank.Queen:
				rank = 2;
				break;
			case CardRank.King:
				rank = 1;
				break;
			default:
				break;
			}

			if (rank == -1 || suit == -1)
			{
				return 54;
			}

			return 4 * rank + suit + 1;
		}
		public string ImagePath()
		{
			return "assets\\" + ImageNum().ToString() + ".png";
		}

		public override int GetHashCode()
		{
			return ((int)Suit << 2) + (int)Rank;
		}

		public override string ToString()
		{
			return Rank.ToString() + " of " + Suit.ToString();
		}

		public string MakeCardID()
		{
			return Suit.ToString() + "." + Rank.ToString();
		}

		public static Card FromString(string key)
		{
			string upper = key.ToUpper();

			char last = upper.Last();
			CardSuit? suit = null;
			switch (last)
			{
			case 'C':
				suit = CardSuit.Clubs;
				break;
			case 'D':
				suit = CardSuit.Diamonds;
				break;
			case 'H':
				suit = CardSuit.Hearts;
				break;
			case 'S':
				suit = CardSuit.Spades;
				break;
			}

			string rs = upper.Substring(0, upper.Length - 1);

			CardRank? rank = null;
			int test = -1;
			if (int.TryParse(rs, out test))
			{
				rank = (CardRank)test;
			}
			else if (rs == "J")
			{
				rank = CardRank.Jack;
			}
			else if (rs == "Q")
			{
				rank = CardRank.Queen;
			}
			else if (rs == "K")
			{
				rank = CardRank.King;
			}

			if (rank == null || suit == null)
			{
				throw new FormatException(key + " is not a valid card.");
			}

			return new Card(suit.Value, rank.Value);
		}

		/// <summary>
		/// Shortened string
		/// </summary>
		/// <returns></returns>
		public string ToCustomString()
		{
			return Rank.ToCustomString() + Suit.ToCustomString();
		}
	}

	public static class CardExtensions
	{
		public static Card NextCard(this Random rand)
		{
			CardSuit suit = (CardSuit)rand.Next((int)CardSuit.Spades + 1);
			CardRank rank = (CardRank)rand.Next((int)CardRank.King) + 1;

			return new Card(suit, rank);
		}
	}
}
